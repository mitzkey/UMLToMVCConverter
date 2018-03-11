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

        private static ExtendedType GetDetailedType(Type type, string multiplicityLowerBound, string multiplicityUpperBound)
        {
            if (!string.IsNullOrWhiteSpace(multiplicityUpperBound) 
                && (multiplicityUpperBound == "*"
                    || Convert.ToInt32(multiplicityUpperBound) > 1))
            {
                return new ExtendedType(typeof(ICollection<>), true, true, new List<Type> { type }, true);
            }

            if (string.IsNullOrWhiteSpace(multiplicityLowerBound) || Convert.ToInt32(multiplicityLowerBound) == 0)
            {
                return GetNullableType(type);
            }

            return new ExtendedType(type, true);
        }

        public static ExtendedType UmlToCsharpNullable(XElement xProperty)
        {
            var innerType = xProperty.OptionalAttributeValue("type");

            if (innerType == null)
            {
                var xType = xProperty.Descendants("type").FirstOrDefault();
                Insist.IsNotNull(xType, nameof(xType));
                var xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
                var umlType = xRefExtension.ObligatoryAttributeValue("referentPath").Split(':', ':').Last();
                var cSharpType = UmlTypesHelper.UmlToCsharp(umlType, mplLowerVal, mplUpperVal);
                return cSharpType;
            }
            else
            {
                var xReturnTypeElement = this.GetElementById(innerType);
                var typeName = xReturnTypeElement.ObligatoryAttributeValue("name");
                return new ExtendedType(typeName, false);
            }
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

            var isProperty = this.xmiWrapper.IsUmlProperty(xElement);

            if (isProperty)
            {
                var xProperty = xElement;
                var multiplicity = this.xmiWrapper.GetMultiplicity(xProperty);

                switch (multiplicity)
                {
                    case Multiplicity.ZeroOrOne:
                        return this.GetNullableType(xProperty);
                    case Multiplicity.ExactlyOne:
                        return this.GetNotNullableType(xProperty);
                    case Multiplicity.Multiple:
                        return this.GetMultipleType(xProperty);
                    default:
                        throw new Exception("Multiplicity switch failed for property: " + xProperty);
                }
            }

            //do dopisania typ dla nie-propertisów, czyli np. parametrów albo typów zwracanych metod
        }

        private ExtendedType GetNullableType(XElement xProperty)
        {
            if (this.xmiWrapper.IsPropertyOfPrimitiveType(xProperty))
            {
                return this.GetPrimitiveNullableType(xProperty);
            }

            return this.GetComplexType(xProperty);
        }

        private ExtendedType GetPrimitiveNullableType(XElement xProperty)
        {
            var umlType = this.xmiWrapper.GetPrimitiveUmlTypeForProperty(xProperty);
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

        private ExtendedType GetComplexType(XElement xProperty)
        {
            var innerType = xProperty.OptionalAttributeValue("type");
            var xInnerTypeElement = this.xmiWrapper.GetXElementById(innerType);
            var typeName = xInnerTypeElement.ObligatoryAttributeValue("name");
            return new ExtendedType(typeName, false);
        }
        
        private ExtendedType GetNotNullableType(XElement xProperty)
        {
            if (this.xmiWrapper.IsPropertyOfPrimitiveType(xProperty))
            {
                return GetPrimitiveNonNullableType(xProperty);
                
            }

            return this.GetComplexType(xProperty);
        }

        private ExtendedType GetPrimitiveNonNullableType(XElement xProperty)
        {
            var umlType = this.xmiWrapper.GetPrimitiveUmlTypeForProperty(xProperty);
            var type = MapPrimitiveType(umlType);
            return new ExtendedType(type, true);
        }

        private ExtendedType CreateAndGetPrimitiveTypeEntity(XElement xProperty)
        {
            var name = xProperty.ObligatoryAttributeValue("name");

            var codeTypeDeclaration = new CodeTypeDeclaration(name)
            {
                IsStruct = true,
                TypeAttributes = TypeAttributes.Public
            };
            
            var cSharpType = this.GetPrimitiveNonNullableType(xProperty);
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

        private ExtendedType GetMultipleType(XElement xProperty)
        {
            if (this.xmiWrapper.IsPropertyOfPrimitiveType(xProperty))
            {
                var newPrimitiveTypeEntity = this.CreateAndGetPrimitiveTypeEntity(xProperty);
                return this.GetCollectionTypeFor(newPrimitiveTypeEntity);
            }

            var complexType = this.GetComplexType(xProperty);

            return this.GetCollectionTypeFor(complexType);
        }

        private ExtendedType GetCollectionTypeFor(ExtendedType extType)
        {
            return new ExtendedType(typeof(ICollection<>), true, true, new List<Type> { extType.Type }, true);
        }
    }
}
