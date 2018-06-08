namespace UMLToMVCConverter.UMLHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Common.XmiTools;
    using UMLToMVCConverter.Common.XmiTools.Interfaces;
    using UMLToMVCConverter.Models;
    using UMLToMVCConverter.Models.Repositories.Interfaces;
    using UMLToMVCConverter.UMLHelpers.Interfaces;

    public class UmlTypesHelper : IUmlTypesHelper
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
                { "real", typeof(double) },
                { "unlimitednatural", typeof(long) },
                { "boolean", typeof(bool) }
            });

        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public UmlTypesHelper(IXmiWrapper xmiWrapper, ITypesRepository typesRepository)
        {
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
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

            return "uml:Class".Equals(sType) || "uml:AssociationClass".Equals(sType);
        }

        public bool IsStruct(XElement type)
        {
            var sType = this.xmiWrapper.ObligatoryAttributeValueWithNamespace(type, "type");

            return "uml:DataType".Equals(sType);
        }

        public bool IsEnum(XElement type)
        {
            var sType = this.xmiWrapper.ObligatoryAttributeValueWithNamespace(type, "type");

            return "uml:Enumeration".Equals(sType);
        }

        public bool IsAbstract(XElement type)
        {
            return Convert.ToBoolean(type.OptionalAttributeValue("isAbstract"));
        }

        public TypeReference GetXElementCsharpType(XElement xElement)
        {
            Insist.IsNotNull(xElement, nameof(xElement));

            if (!XmiWrapper.CanHaveType(xElement))
            {
                throw new Exception("No type matching strategy for XElement:\n" + xElement);
            }

            if (this.IsVoidType(xElement))
            {
                return GetVoidType();
            }

            var multiplicity = this.xmiWrapper.GetMultiplicity(xElement);

            switch (multiplicity)
            {
                case Multiplicity.ZeroOrOne:
                    return this.GetNullableType(xElement);
                case Multiplicity.ExactlyOne:
                    return this.GetForNotNullableType(xElement);
                case Multiplicity.ZeroOrMore:
                    return this.GetMultipleType(xElement);
                case Multiplicity.OneOrMore:
                    return this.GetMultipleType(xElement);
                default:
                    throw new Exception("Multiplicity not found for property: " + xElement);
            }
        }

        private TypeReference GetVoidType()
        {
            var typeReferenceBuilder = TypeReference.Builder();
            return typeReferenceBuilder
                .SetType(typeof(void))
                .IsBaseType(true)
                .Build();
        }

        private bool IsVoidType(XElement xElement)
        {
            if (!this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                return false;
            }

            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var cSharpType = MapPrimitiveType(umlType);

            return cSharpType == typeof(void);
        }

        private TypeReference GetNullableType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                return this.GetPrimitiveNullableType(xElement);
            }

            return this.GetComplexType(xElement, true);
        }

        private TypeReference GetPrimitiveNullableType(XElement xElement)
        {
            var typeReferenceBuilder = TypeReference.Builder();
            typeReferenceBuilder.IsBaseType(true);
            typeReferenceBuilder.IsNullable(true);

            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var cSharpType = MapPrimitiveType(umlType);

            var isPrimitive = !(cSharpType == typeof(string));
            typeReferenceBuilder.IsPrimitive(isPrimitive);

            if (cSharpType.IsValueType)
            {
                var genericTypeReferenceBuilder = TypeReference.Builder();
                var genericTypeReference = genericTypeReferenceBuilder
                    .SetType(cSharpType)
                    .IsBaseType(true)
                    .Build();
                return typeReferenceBuilder
                    .SetType(typeof(Nullable))
                    .IsGeneric(true)
                    .SetGeneric(genericTypeReference)
                    .Build();
            }

            typeReferenceBuilder.SetType(cSharpType);
            return typeReferenceBuilder.Build();
        }

        private TypeReference GetComplexType(XElement xElement, bool isNullable)
        {
            var typeReferenceBuilder = TypeReference.Builder();

            var innerType = xElement.OptionalAttributeValue("type");
            var xInnerTypeElement = this.xmiWrapper.GetXElementById(innerType);
            var typeName = xInnerTypeElement.ObligatoryAttributeValue("name");

            return typeReferenceBuilder
                .SetName(typeName)
                .IsBaseType(false)
                .SetReferenceTypeXmiId(innerType)
                .IsComplexType(true)
                .IsNullable(isNullable)
                .Build();
        }
        
        private TypeReference GetForNotNullableType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                return this.GetPrimitiveNonNullableType(xElement);
                
            }

            var isNullable = false;
            return this.GetComplexType(xElement, isNullable);
        }

        private TypeReference GetPrimitiveNonNullableType(XElement xElement)
        {
            var typeReferenceBuilder = TypeReference.Builder();

            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var cSharpType = MapPrimitiveType(umlType);

            var isPrimitive = !(cSharpType == typeof(string));
            typeReferenceBuilder.IsPrimitive(isPrimitive);

            return typeReferenceBuilder
                .SetType(cSharpType)
                .IsBaseType(true)
                .IsPrimitive(isPrimitive)
                .Build();
        }

        private TypeReference CreateAndGetPrimitiveTypeEntity(XElement xElement)
        {
            var typeReferenceBuilder = TypeReference.Builder();

            var name = xElement.ObligatoryAttributeValue("name");
            name = name.FirstCharToUpper();

            var isClass = true;
            var visibility = CSharpVisibilityString.Public;
            var codeTypeDeclaration = new TypeModel(name, isClass, visibility);

            var valueType = this.GetPrimitiveNonNullableType(xElement);

            var valueProperty = Property.Builder()
                .SetName("Value")
                .SetTypeReference(valueType)
                .SetVisibility(CSharpVisibilityString.Public)
                .Build();


            codeTypeDeclaration.Properties.Add(valueProperty);

            this.typesRepository.Add(codeTypeDeclaration);

            return typeReferenceBuilder
                .SetName(name)
                .IsBaseType(true)
                .Build();
        }

        private TypeReference GetMultipleType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                var newPrimitiveTypeEntity = this.CreateAndGetPrimitiveTypeEntity(xElement);
                return this.GetCollectionTypeFor(newPrimitiveTypeEntity, true);
            }

            var complexType = this.GetComplexType(xElement, true);

            return this.GetCollectionTypeFor(complexType, false);
        }

        private TypeReference GetCollectionTypeFor(TypeReference typeReference, bool isPrimitive)
        {
            var typeReferenceBuilder = TypeReference.Builder();

            return typeReferenceBuilder
                .SetType(typeof(ICollection<>))
                .IsBaseType(isPrimitive)
                .IsGeneric(true)
                .SetGeneric(typeReference)
                .IsCollection(true)
                .Build();
        }
    }
}
