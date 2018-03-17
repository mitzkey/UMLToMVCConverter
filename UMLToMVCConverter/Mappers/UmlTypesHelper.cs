using System;
using System.Collections.Generic;
using UMLToMVCConverter.ExtendedTypes;

namespace UMLToMVCConverter.Mappers
{
    using System.CodeDom;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    public class UmlTypesHelper
    {
        private static readonly IReadOnlyDictionary<string, Type> PrimitiveTypesMap = new ReadOnlyDictionary<string, Type>(
            new Dictionary<string, Type>
            {
                { "date", typeof(DateTime) },
                { "string", typeof(string) },
                { "integer", typeof(int) },
                { "double", typeof(double) },
                { "void", typeof(void) },
                { "int", typeof(int) },
                { "boolean", typeof(bool) }
            });

        private readonly XmiWrapper xmiWrapper;
        private readonly List<CodeTypeDeclaration> codeTypeDeclarations;

        public UmlTypesHelper(XmiWrapper xmiWrapper, List<CodeTypeDeclaration> types)
        {
            this.codeTypeDeclarations = types;
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
                case Multiplicity.Multiple:
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
            var umlType = this.xmiWrapper.GetPrimitiveUmlTypeForProperty(xElement);
            var type = MapPrimitiveType(umlType);
            var returnType = Nullable.GetUnderlyingType(type);
            if (returnType != null)
            {
                return new ExtendedType(type, true);
            }

            if (type.IsValueType)
            {
                return new ExtendedType(typeof(Nullable), true, true, new List<Type> { type });
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
                return GetPrimitiveNonNullableType(xElement);
                
            }

            return this.GetComplexType(xElement);
        }

        private ExtendedType GetPrimitiveNonNullableType(XElement xElement)
        {
            var umlType = this.xmiWrapper.GetPrimitiveUmlTypeForProperty(xElement);
            var type = MapPrimitiveType(umlType);
            return new ExtendedType(type, true);
        }

        private ExtendedType CreateAndGetPrimitiveTypeEntity(XElement xElement)
        {
            var name = xElement.ObligatoryAttributeValue("name");

            var codeTypeDeclaration = new CodeTypeDeclaration(name)
            {
                IsStruct = true,
                TypeAttributes = TypeAttributes.Public
            };
            
            var cSharpType = this.GetPrimitiveNonNullableType(xElement);
            var typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

            var valueProperty = new ExtendedCodeMemberProperty
            {
                Name = "Value",
                Type = typeRef,
                Attributes = MemberAttributes.Public
            };

            codeTypeDeclaration.Members.Add(valueProperty);

            this.codeTypeDeclarations.Add(codeTypeDeclaration);

            return typeRef.ExtType;
        }

        private ExtendedType GetMultipleType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                var newPrimitiveTypeEntity = this.CreateAndGetPrimitiveTypeEntity(xElement);
                return this.GetCollectionTypeFor(newPrimitiveTypeEntity);
            }

            var complexType = this.GetComplexType(xElement);

            return this.GetCollectionTypeFor(complexType);
        }

        private ExtendedType GetCollectionTypeFor(ExtendedType extType)
        {
            return new ExtendedType(typeof(ICollection<>), true, true, new List<Type> { extType.Type }, true);
        }
    }
}
