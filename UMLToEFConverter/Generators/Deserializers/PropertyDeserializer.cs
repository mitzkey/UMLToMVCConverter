﻿namespace UMLToEFConverter.Generators.Deserializers
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToEFConverter.Common;
    using UMLToEFConverter.Common.XmiTools;
    using UMLToEFConverter.Common.XmiTools.Interfaces;
    using UMLToEFConverter.Generators.Deserializers.Interfaces;
    using UMLToEFConverter.Models;
    using UMLToEFConverter.Models.Repositories.Interfaces;
    using UMLToEFConverter.UMLHelpers;
    using UMLToEFConverter.UMLHelpers.Interfaces;
    using Attribute = UMLToEFConverter.Models.Attribute;

    public class PropertyDeserializer : IPropertyDeserializer
    {
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IXAttributeNameResolver xAttributeNameResolver;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public PropertyDeserializer(IUmlTypesHelper umlTypesHelper, IXAttributeNameResolver xAttributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper, IXmiWrapper xmiWrapper, ITypesRepository typesRepository, IAssociationDeserializer associationDeserializer, IAssociationsRepository associationsRepository, ILogger logger)
        {
            this.umlTypesHelper = umlTypesHelper;
            this.xAttributeNameResolver = xAttributeNameResolver;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
        }

        public Property Create(TypeModel type, XElement xProperty)
        {
            Insist.IsNotNull(xProperty, nameof(xProperty));

            var propertyBuilder = Property.Builder();

            var propertyName = this.xAttributeNameResolver.GetName(xProperty);
            propertyBuilder.SetName(propertyName);
          
            var umlVisibility = xProperty.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharpString(umlVisibility);
            var visibility = cSharpVisibility;
            propertyBuilder.SetVisibility(visibility);

            var isStatic = Convert.ToBoolean(xProperty.OptionalAttributeValue("isStatic"));
            propertyBuilder.IsStatic(isStatic);
            var isDerived = Convert.ToBoolean(xProperty.OptionalAttributeValue("isDerived"));
            if (isDerived)
            {
                propertyBuilder.IsReadOnly(true);
                propertyBuilder.WithAttribute(new Attribute("NotMapped", null));
            }
            propertyBuilder.IsDerived(isDerived);

            var cSharpTypeReference = this.umlTypesHelper.GetXElementCsharpType(xProperty);
            propertyBuilder.SetTypeReference(cSharpTypeReference);
            int? defaultValueKey = null;
            string defaultValueString = null;
            var xDefaultValue = xProperty.Element("defaultValue");
            if (xDefaultValue != null)
            {
                if (cSharpTypeReference.IsReferencingXmiDeclaredType &&
                    this.typesRepository.GetTypeByXmiId(cSharpTypeReference.ReferenceTypeXmiID).IsEnum)
                {
                    var instance = this.xmiWrapper.GetXElementById(xDefaultValue.ObligatoryAttributeValue("instance"));
                    var instanceValue = instance.ObligatoryAttributeValue("name");
                    var instanceOwnerId = this.xmiWrapper.GetElementsId(instance.Parent);
                    var instanceOwnerType = this.typesRepository.GetTypeByXmiId(instanceOwnerId);
                    var literal = instanceOwnerType.Literals.Single(x => x.Value == instanceValue).Key;
                    defaultValueKey = literal;
                }
                else
                {

                    var extendedType = cSharpTypeReference;
                    if (extendedType.IsGeneric && !extendedType.IsPrimitive)
                    {
                        throw new NotSupportedException("No default value for generic types supported");
                    }

                    defaultValueString = this.GetDefaultValueString(xDefaultValue);
                }
            }
            propertyBuilder.SetDefaultValueKey(defaultValueKey);
            propertyBuilder.SetDefaultValueString(defaultValueString);

            var isID = Convert.ToBoolean(xProperty.OptionalAttributeValue("isID"));
            if (isID)
            {
                propertyBuilder.IsID(isID);
                propertyBuilder.WithAttribute(
                    new Attribute("DatabaseGenerated", "DatabaseGeneratedOption.None", false));
            }

            var multiplicity = this.xmiWrapper.GetMultiplicity(xProperty);
            if (multiplicity == Multiplicity.ExactlyOne && !cSharpTypeReference.IsPrimitive)
            {
                var attribute = new Attribute("Required", null);
                propertyBuilder.WithAttribute(attribute);
            }

            if (cSharpTypeReference.IsReferencingXmiDeclaredType)
            {
                var referencingType = typesRepository.GetTypeByXmiId(cSharpTypeReference.ReferenceTypeXmiID);
                if (referencingType.IsEnum)
                {
                    propertyBuilder.IsReferencingEnumType(true);
                }
            }

            var property = propertyBuilder.Build();

            if (isID)
            {
                type.PrimaryKeyAttributes.Add(property);
            }

            return property;
        }

        public Property CreateBasicProperty(string name, Type type, Type genericType = null)
        {
            var propertyBuilder = Property.Builder();

            var isGeneric = genericType != null;

            var cSharpTypeReferenceBuilder = TypeReference.Builder();
            cSharpTypeReferenceBuilder
                .SetType(type)
                .IsBaseType(true);

            if (isGeneric)
            {
                var genericTypeReference = TypeReference.Builder()
                    .SetType(genericType)
                    .IsBaseType(true)
                    .Build();
                cSharpTypeReferenceBuilder
                    .IsGeneric(true)
                    .SetGeneric(genericTypeReference);
            }

            var typeReference = cSharpTypeReferenceBuilder.Build();

            var property = propertyBuilder
                .SetName(name)
                .SetTypeReference(typeReference)
                .SetVisibility(CSharpVisibilityString.Public)
                .Build();

            return property;
        }

        private string GetDefaultValueString(XElement xDefaultValue)
        {
            var defaultValueType = this.xmiWrapper.GetXElementType(xDefaultValue);

            switch (defaultValueType)
            {
                case XElementType.LiteralString:
                    return xDefaultValue.ObligatoryAttributeValue("value");
                case XElementType.LiteralInteger:
                    return xDefaultValue.OptionalAttributeValue("value") ?? "0";
                case XElementType.LiteralBoolean:
                    return xDefaultValue.OptionalAttributeValue("value") ?? "false";
                case XElementType.LiteralUnlimitedNatural:
                    return xDefaultValue.OptionalAttributeValue("value") ?? "0";
                case XElementType.InstanceValue:
                    var instance = this.xmiWrapper.GetXElementById(xDefaultValue.ObligatoryAttributeValue("instance"));
                    var instanceValue = instance.ObligatoryAttributeValue("name");
                    var instanceOwnerId = this.xmiWrapper.GetElementsId(instance.Parent);
                    var instanceOwnerType = this.typesRepository.GetTypeByXmiId(instanceOwnerId);
                    var literal = instanceOwnerType.Literals.Single(x => x.Value == instanceValue).Value;
                    return $"{instanceOwnerType.Name}.{literal}";
                default:
                    throw new NotImplementedException($"Unhandled xElement type for default value: {xDefaultValue}");
            }
        }
    }
}