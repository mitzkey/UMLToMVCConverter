namespace UMLToMVCConverter
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;

    public class UmlTypesHelper : IUmlTypesHelper
    {
        public List<ExtendedCodeTypeDeclaration> CodeTypeDeclarations { get; set; }

        private static readonly IReadOnlyDictionary<string, Type> PrimitiveTypesMap = new ReadOnlyDictionary<string, Type>(
            new Dictionary<string, Type>
            {
                { "date", typeof(DateTime) },
                { "string", typeof(string) },
                { "integer", typeof(int) },
                { "double", typeof(double) },
                { "void", typeof(void) },
                { "int", typeof(int) },
                { "real", typeof(double) },
                { "boolean", typeof(bool) }
            });

        private readonly IXmiWrapper xmiWrapper;

        public UmlTypesHelper(IXmiWrapper xmiWrapper)
        {
            this.xmiWrapper = xmiWrapper;
        }

        public static Type MapPrimitiveType(string umlType)
        {
            var mappedType = PrimitiveTypesMap[umlType.ToLower()];

            if (mappedType == null)
            {
                throw new Exception("Mapping of primitive type: " + umlType + " unsupported.");
            }

            return mappedType;
        }

        public bool IsClass(XElement type)
        {
            var sType = this.xmiWrapper.ObligatoryAttributeValueWithNamespace(type, "type");

            return "uml:Class".Equals(sType);
        }

        public bool IsStruct(XElement type)
        {
            var sType = this.xmiWrapper.ObligatoryAttributeValueWithNamespace(type, "type");

            return "uml:DataType".Equals(sType);
        }

        public bool IsAbstract(XElement type)
        {
            return Convert.ToBoolean(type.OptionalAttributeValue("isAbstract"));
        }

        public ExtendedType GetXElementCsharpType(XElement xElement)
        {
            Insist.IsNotNull(xElement, nameof(xElement));

            if (!XmiWrapper.CanHaveType(xElement))
            {
                throw new Exception("No type matching strategy for XElement:\n" + xElement);
            }

            var multiplicity = this.xmiWrapper.GetMultiplicity(xElement);

            switch (multiplicity)
            {
                case Multiplicity.ZeroOrOne:
                    return this.GetNullableType(xElement);
                case Multiplicity.ExactlyOne:
                    return this.GetNotNullableType(xElement);
                case Multiplicity.ZeroOrMore:
                    return this.GetMultipleType(xElement);
                case Multiplicity.OneOrMore:
                    return this.GetMultipleType(xElement);
                default:
                    throw new Exception("Multiplicity not found for property: " + xElement);
            }
        }

        private ExtendedType GetNullableType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                return this.GetPrimitiveNullableType(xElement);
            }

            return this.GetComplexType(xElement);
        }

        private ExtendedType GetPrimitiveNullableType(XElement xElement)
        {
            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var type = MapPrimitiveType(umlType);

            if (type == typeof(void))
            {
                return ExtendedType.Void;
            }

            var returnType = Nullable.GetUnderlyingType(type);
            if (returnType != null)
            {
                return new ExtendedType(type, true);
            }

            if (type.IsValueType)
            {
                var generic = new ExtendedType(type, true);
                return new ExtendedType(typeof(Nullable), true, true, new List<ExtendedType> { generic });
            }

            return new ExtendedType(type, true);
        }

        private ExtendedType GetComplexType(XElement xElement)
        {
            var innerType = xElement.OptionalAttributeValue("type");
            var xInnerTypeElement = this.xmiWrapper.GetXElementById(innerType);
            var typeName = xInnerTypeElement.ObligatoryAttributeValue("name");
            return new ExtendedType(typeName, false);
        }
        
        private ExtendedType GetNotNullableType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                return this.GetPrimitiveNonNullableType(xElement);
                
            }

            return this.GetComplexType(xElement);
        }

        private ExtendedType GetPrimitiveNonNullableType(XElement xElement)
        {
            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var type = MapPrimitiveType(umlType);
            return new ExtendedType(type, true);
        }

        private ExtendedType CreateAndGetPrimitiveTypeEntity(XElement xElement)
        {
            var name = xElement.ObligatoryAttributeValue("name");
            name = name.FirstCharToUpper();

            var codeTypeDeclaration = new ExtendedCodeTypeDeclaration(name)
            {
                IsStruct = false,
                TypeAttributes = TypeAttributes.Public
            };
            
            var entityType = new ExtendedType(name, true);

            var valueType = this.GetPrimitiveNonNullableType(xElement);
            var valueCodeTypeReference = new ExtendedCodeTypeReference(valueType);
            var valueProperty = new ExtendedCodeMemberProperty
            {
                Name = "Value",
                Type = valueCodeTypeReference,
                Attributes = MemberAttributes.Public
            };

            codeTypeDeclaration.Members.Add(valueProperty);

            this.CodeTypeDeclarations.Add(codeTypeDeclaration);

            return entityType;
        }

        private ExtendedType GetMultipleType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                var newPrimitiveTypeEntity = this.CreateAndGetPrimitiveTypeEntity(xElement);
                return this.GetCollectionTypeFor(newPrimitiveTypeEntity, true);
            }

            var complexType = this.GetComplexType(xElement);

            return this.GetCollectionTypeFor(complexType, false);
        }

        private ExtendedType GetCollectionTypeFor(ExtendedType extType, bool isPrimitive)
        {
            return new ExtendedType(typeof(ICollection<>), isPrimitive, true, new List<ExtendedType> { extType }, true);
        }
    }
}
