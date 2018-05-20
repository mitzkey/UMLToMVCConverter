namespace UMLToMVCConverter.XmiTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain;

    public class XmiWrapper : IXmiWrapper
    {
        private readonly XDocument xmiDocument;
        private readonly IXAttributeEqualityComparer attributeEqualityComparer;
        private readonly XNamespace xmiNamespace;
        private readonly XNamespace umlNamespace;

        public XmiWrapper(XDocument xmiDocument, IXAttributeEqualityComparer attributeEqualityComparer)
        {
            this.xmiDocument = xmiDocument;
            this.xmiNamespace = this.xmiDocument.Root.GetNamespaceOfPrefix("xmi");
            this.umlNamespace = this.xmiDocument.Root.GetNamespaceOfPrefix("uml");
            this.attributeEqualityComparer = attributeEqualityComparer;
        }

        public IEnumerable<XElement> GetXUmlModels()
        {
            return this.xmiDocument.Descendants(this.umlNamespace + "Model").ToList();
        }

        public IEnumerable<XElement> GetXTypes(XElement umlModel)
        {
            return umlModel.Descendants()
                .Where(i => i.Attributes().
                                Contains(new XAttribute(this.xmiNamespace + "type", "uml:Class"), this.attributeEqualityComparer)
                            || i.Attributes().Contains(new XAttribute(this.xmiNamespace + "type", "uml:DataType"), this.attributeEqualityComparer)
                            || i.Attributes().Contains(new XAttribute(this.xmiNamespace + "type", "uml:Enumeration"), this.attributeEqualityComparer));
        }

        public XElement GetXTypeGeneralization(XElement type)
        {
            return type
                .Descendants("generalization")
                .FirstOrDefault(i => i.Attributes()
                    .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Generalization"), this.attributeEqualityComparer));
        }

        public XElement GetXElementById(string id)
        {
            return this.xmiDocument.Descendants()
                .SingleOrDefault(e => id.Equals(e.OptionalAttributeValue(this.xmiNamespace + "id")));
        }

        public string ObligatoryAttributeValueWithNamespace(XElement type, string s)
        {
            return type.ObligatoryAttributeValue(this.xmiNamespace + s);
        }

        public IEnumerable<XElement> GetXAssociations(XElement umlModel)
        {
            return umlModel.Descendants()
                .Where(i => i.Attributes().
                                Contains(new XAttribute(this.xmiNamespace + "type", "uml:Association"), this.attributeEqualityComparer));
        }

        public IEnumerable<XElement> GetAssociationEndsXElements(XElement xAssociation)
        {
            var firstEndId = xAssociation
                .Descendants("memberEnd")
                .First()
                .ObligatoryAttributeValue(this.xmiNamespace + "idref");
            var secondEndId = xAssociation
                .Descendants("memberEnd")
                .Single(x => x.ObligatoryAttributeValue(this.xmiNamespace + "idref") != firstEndId)
                .ObligatoryAttributeValue(this.xmiNamespace + "idref");

            var firstEnd = this.GetXElementById(firstEndId);
            var secondEnd = this.GetXElementById(secondEndId);

            return new List<XElement> { firstEnd, secondEnd };
        }

        public string GetElementsId(XElement xElement)
        {
            return xElement.ObligatoryAttributeValue(this.xmiNamespace + "id");
        }

        public IEnumerable<XElement> GetXAggregations(XElement xUmlModel)
        {
            var xAggregations = new List<XElement>();

            var xAssociations = this.GetXAssociations(xUmlModel);

            foreach (var xAssociation in xAssociations)
            {
                var associationEnds = this.GetAssociationEndsXElements(xAssociation).ToList();

                if (associationEnds.Any(x => !string.IsNullOrWhiteSpace(x.OptionalAttributeValue("aggregation"))))
                {
                    xAggregations.Add(xAssociation);
                }
            }

            return xAggregations;
        }

        public IEnumerable<XElement> GetLiterals(XElement xType)
        {
            return xType.Descendants("ownedLiteral");
        }

        public XElementType GetXElementType(XElement xElement)
        {
            var typeString = xElement.Attribute(this.xmiNamespace + "type").Value;

            switch (typeString)
            {
                case "uml:InstanceValue":
                    return XElementType.InstanceValue;
                case "uml:LiteralString":
                    return XElementType.LiteralString;
                case "uml:Enumeration":
                    return XElementType.Enumeration;
                case "uml:Class":
                    return XElementType.Class;
                case "uml:DataType":
                    return XElementType.DataType;
                case "uml:LiteralInteger":
                    return XElementType.LiteralInteger;
                default:
                    throw new NotImplementedException($"Uknown xElement type: {typeString}");
            }
        }

        public XElement GetOppositeAssociationEnd(string associationId, string xElementId)
        {
            var association = this.GetXElementById(associationId);
            var associationEndsXElements = this.GetAssociationEndsXElements(association);
            return associationEndsXElements.Single(x => !xElementId.Equals(this.GetElementsId(x)));
        }

        public IEnumerable<XElement> GetXAttributes(XElement type)
        {
            return type.Descendants("ownedAttribute")
                .Where(this.IsUmlProperty);
        }

        public IEnumerable<XElement> GetXOperations(XElement type)
        {
            return type.Descendants("ownedOperation")
                .Where(
                    i => i.Attributes()
                        .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Operation"), this.attributeEqualityComparer));
        }

        public XElement GetXReturnParameter(XElement operation)
        {
            return operation.Descendants("ownedParameter")
                .SingleOrDefault(i => "return".Equals(i.OptionalAttributeValue("direction")));
        }

        public IEnumerable<XElement> GetXParameters(XElement operation)
        {
            return operation
                .Descendants("ownedParameter")
                .Where(i => i.Attribute("direction") == null 
                    || !i.ObligatoryAttributeValue("direction").Equals("return"));
        }

        public bool IsUmlProperty(XElement xElement)
        {
            return xElement
                .Attributes()
                .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Property"), this.attributeEqualityComparer);
        }

        public string GetPrimitiveUmlType(XElement xProperty)
        {
            var xType = xProperty.Descendants("type").FirstOrDefault();

            Insist.IsNotNull(xType, nameof(xType));
            var xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
            var umlType = xRefExtension.ObligatoryAttributeValue("referentPath").Split(':', ':').Last();
            return umlType;
        }

        public bool IsOfPrimitiveType(XElement xElement)
        {
            var xType = xElement.Descendants("type").FirstOrDefault();
            return xType != null;
        }

        public static bool CanHaveType(XElement xElement)
        {
            return xElement.Name.ToString().Equals("ownedAttribute")
                   || xElement.Name.ToString().Equals("ownedParameter");
        }

        public Multiplicity GetMultiplicity(XElement attribute)
        {
            var lv = attribute.Descendants("lowerValue").SingleOrDefault();
            var multiplicityLowerBound = lv?.OptionalAttributeValue("value");
            var uv = attribute.Descendants("upperValue").SingleOrDefault();
            var multiplicityUpperBound = uv?.OptionalAttributeValue("value");

            if (!string.IsNullOrWhiteSpace(multiplicityUpperBound)
                && (multiplicityUpperBound == "*"
                    || Convert.ToInt32(multiplicityUpperBound) > 1))
            {
                if (!string.IsNullOrWhiteSpace(multiplicityLowerBound)
                    && Convert.ToInt32(multiplicityLowerBound) > 0)
                {
                    return Multiplicity.OneOrMore;
                }

                return Multiplicity.ZeroOrMore;
            }

            if (string.IsNullOrWhiteSpace(multiplicityLowerBound) || Convert.ToInt32(multiplicityLowerBound) == 0)
            {
                return Multiplicity.ZeroOrOne;
            }

            return Multiplicity.ExactlyOne;
        }
    }
}
