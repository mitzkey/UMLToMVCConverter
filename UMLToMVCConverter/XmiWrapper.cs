﻿namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class XmiWrapper
    {
        private readonly XDocument xmi;
        private readonly AttributeEqualityComparer attributeEqualityComparer;
        private readonly XNamespace xmiNamespace;
        private readonly XNamespace umlNamespace;
        private static readonly List<string> XElementsWithTypes = new List<string>
        {
            "property",
            "parameter"
        };

        public XmiWrapper(XDocument xmi, XNamespace xmiNamespace, XNamespace umlNamespace, AttributeEqualityComparer attributeEqualityComparer)
        {
            this.umlNamespace = umlNamespace;
            this.xmiNamespace = xmiNamespace;
            this.attributeEqualityComparer = attributeEqualityComparer;
            this.xmi = xmi;
        }

        public IEnumerable<XElement> GetXUmlModels()
        {
            return this.xmi.Descendants(this.umlNamespace + "Model").ToList();
        }

        public IEnumerable<XElement> GetXTypes(XElement umlModel)
        {
            return umlModel.Descendants()
                .Where(i => i.Attributes().
                                Contains(new XAttribute(this.xmiNamespace + "type", "uml:Class"), this.attributeEqualityComparer)
                            || i.Attributes().Contains(new XAttribute(this.xmiNamespace + "type", "uml:DataType"), this.attributeEqualityComparer))
                .ToList();
        }

        public XElement GetXClassGeneralization(XElement type)
        {
            return type
                .Descendants("generalization")
                .FirstOrDefault(i => i.Attributes()
                    .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Generalization"), this.attributeEqualityComparer));
        }

        public XElement GetXElementById(string id)
        {
            return this.xmi.Descendants()
                .SingleOrDefault(e => id.Equals(e.OptionalAttributeValue(this.xmiNamespace + "id")));
        }

        public string ObligatoryAttributeValueWithNamespace(XElement type, string s)
        {
            return type.ObligatoryAttributeValue(this.xmiNamespace + s);
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
                .Contains(new XAttribute(xmiNamespace + "type", "uml:Property"), this.attributeEqualityComparer);
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
                return Multiplicity.Multiple;
            }

            if (string.IsNullOrWhiteSpace(multiplicityLowerBound) || Convert.ToInt32(multiplicityLowerBound) == 0)
            {
                return Multiplicity.ZeroOrOne;
            }

            return Multiplicity.ExactlyOne;
        }
    }
}
