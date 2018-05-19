﻿namespace UMLToMVCConverter.UMLHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.XmiTools;

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

            return "uml:Class".Equals(sType);
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
                    return this.GetNotNullableType(xElement);
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

            return this.GetComplexType(xElement);
        }

        private TypeReference GetPrimitiveNullableType(XElement xElement)
        {
            var typeReferenceBuilder = TypeReference.Builder();

            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var cSharpType = MapPrimitiveType(umlType);

            if (cSharpType.IsValueType)
            {
                var generic = new TypeReference(cSharpType, true);
                return typeReferenceBuilder
                    .SetType(typeof(Nullable))
                    .IsBaseType(true)
                    .IsGeneric(true)
                    .SetGenerics(generic)
                    .Build();
            }

            typeReferenceBuilder.SetType(cSharpType);
            typeReferenceBuilder.IsBaseType(true);
            return typeReferenceBuilder.Build();
        }

        private TypeReference GetComplexType(XElement xElement)
        {
            var typeReferenceBuilder = TypeReference.Builder();

            var innerType = xElement.OptionalAttributeValue("type");
            var xInnerTypeElement = this.xmiWrapper.GetXElementById(innerType);
            var typeName = xInnerTypeElement.ObligatoryAttributeValue("name");

            return typeReferenceBuilder
                .SetName(typeName)
                .IsBaseType(false)
                .SetReferenceTypeXmiId(innerType)
                .Build();
        }
        
        private TypeReference GetNotNullableType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                return this.GetPrimitiveNonNullableType(xElement);
                
            }

            return this.GetComplexType(xElement);
        }

        private TypeReference GetPrimitiveNonNullableType(XElement xElement)
        {
            var umlType = this.xmiWrapper.GetPrimitiveUmlType(xElement);
            var type = MapPrimitiveType(umlType);
            return new TypeReference(type, true);
        }

        private TypeReference CreateAndGetPrimitiveTypeEntity(XElement xElement)
        {
            var name = xElement.ObligatoryAttributeValue("name");
            name = name.FirstCharToUpper();

            var isClass = true;
            var visibility = "public";
            var codeTypeDeclaration = new TypeModel(name, isClass, visibility);
            
            var entityType = new TypeReference(name, true);

            var valueType = this.GetPrimitiveNonNullableType(xElement);
            var valueProperty = new Property(
                "Value",
                valueType,
                this.typesRepository,
                true,
                "public",
                false);

            codeTypeDeclaration.Properties.Add(valueProperty);

            this.typesRepository.Add(codeTypeDeclaration);

            return entityType;
        }

        private TypeReference GetMultipleType(XElement xElement)
        {
            if (this.xmiWrapper.IsOfPrimitiveType(xElement))
            {
                var newPrimitiveTypeEntity = this.CreateAndGetPrimitiveTypeEntity(xElement);
                return this.GetCollectionTypeFor(newPrimitiveTypeEntity, true);
            }

            var complexType = this.GetComplexType(xElement);

            return this.GetCollectionTypeFor(complexType, false);
        }

        private TypeReference GetCollectionTypeFor(TypeReference typeReference, bool isPrimitive)
        {
            return new TypeReference(typeof(ICollection<>), isPrimitive, true, new List<TypeReference> { typeReference }, true);
        }
    }
}
