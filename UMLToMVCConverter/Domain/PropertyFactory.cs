namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.UMLHelpers;
    using UMLToMVCConverter.XmiTools;
    using Attribute = UMLToMVCConverter.Domain.Models.Attribute;

    public class PropertyFactory : IPropertyFactory
    {
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IXAttributeNameResolver xAttributeNameResolver;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;
        private readonly IAssociationFactory associationFactory;
        private readonly IAssociationsRepository associationsRepository;
        private readonly ILogger logger;

        public PropertyFactory(IUmlTypesHelper umlTypesHelper, IXAttributeNameResolver xAttributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper, IXmiWrapper xmiWrapper, ITypesRepository typesRepository, IAssociationFactory associationFactory, IAssociationsRepository associationsRepository, ILogger logger)
        {
            this.umlTypesHelper = umlTypesHelper;
            this.xAttributeNameResolver = xAttributeNameResolver;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
            this.associationFactory = associationFactory;
            this.associationsRepository = associationsRepository;
            this.logger = logger;
        }

        public Property Create(TypeModel type, XElement xProperty)
        {
            Insist.IsNotNull(xProperty, nameof(xProperty));

            var propertyBuilder = Property.Builder();
            propertyBuilder.SetTypesRepository(this.typesRepository);

            var propertyName = this.xAttributeNameResolver.GetName(xProperty);
            propertyBuilder.SetName(propertyName);
          
            var umlVisibility = xProperty.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharpString(umlVisibility);
            var visibility = cSharpVisibility;
            propertyBuilder.SetVisibility(visibility);

            var isStatic = Convert.ToBoolean(xProperty.OptionalAttributeValue("isStatic"));
            propertyBuilder.IsStatic(isStatic);

            var hasSet = true;
            var xIsReadonly = Convert.ToBoolean(xProperty.OptionalAttributeValue("isReadOnly"));
            var isDerived = Convert.ToBoolean(xProperty.OptionalAttributeValue("isDerived"));
            if (xIsReadonly || isDerived)
            {
                hasSet = false;
            }
            propertyBuilder.HasSet(hasSet);
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
            propertyBuilder.IsID(isID);

            var multiplicity = this.xmiWrapper.GetMultiplicity(xProperty);
            if (multiplicity == Multiplicity.ExactlyOne && !cSharpTypeReference.IsPrimitive)
            {
                var attribute = new Attribute("Required", null);
                propertyBuilder.WithAttribute(attribute);
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
                .SetTypesRepository(this.typesRepository)
                .HasSet(true)
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
                    return xDefaultValue.ObligatoryAttributeValue("value");
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