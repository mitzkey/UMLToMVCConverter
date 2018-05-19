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

        public PropertyFactory(IUmlTypesHelper umlTypesHelper, IXAttributeNameResolver xAttributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper, IXmiWrapper xmiWrapper, ITypesRepository typesRepository)
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

            var propertyName = this.xAttributeNameResolver.GetName(xProperty);

            var cSharpType = this.umlTypesHelper.GetXElementCsharpType(xProperty);
            
            var umlVisibility = xProperty.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharpString(umlVisibility);
            var visibility = cSharpVisibility;

            var isStatic = Convert.ToBoolean(xProperty.OptionalAttributeValue("isStatic"));

            bool hasSet = true;
            var xIsReadonly = Convert.ToBoolean(xProperty.OptionalAttributeValue("isReadOnly"));
            if (xIsReadonly)
            {
                hasSet = false;
            }

            int? defaultValueKey = null;
            string defaultValueString = null;
            var xDefaultValue = xProperty.Element("defaultValue");
            if (xDefaultValue != null)
            {
                if (cSharpType.IsReferencingXmiDeclaredType &&
                    this.typesRepository.GetTypeByXmiId(cSharpType.ReferenceTypeXmiID).IsEnum)
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

                    var extendedType = cSharpType;
                    if (extendedType.IsGeneric)
                    {
                        throw new NotSupportedException("No default value for generic types supported");
                    }

                    defaultValueString = this.GetDefaultValueString(xDefaultValue);
                }
            }

            var isDerived = Convert.ToBoolean(xProperty.OptionalAttributeValue("isDerived"));
            if (isDerived)
            {
                hasSet = false;
            }

            var isID = Convert.ToBoolean(xProperty.OptionalAttributeValue("isID"));

            var multiplicity = this.xmiWrapper.GetMultiplicity(xProperty);
            var attributes = new List<Attribute>();
            if (multiplicity == Multiplicity.ExactlyOne && cSharpType.IsNullable)
            {
                var attribute = new Attribute("Required");
                attributes.Add(attribute);
            }

            var property = new Property(
                propertyName,
                cSharpType,
                this.typesRepository,
                hasSet,
                visibility,
                isStatic,
                defaultValueKey,
                defaultValueString,
                isDerived,
                isID,
                attributes);

            if (isID)
            {
                type.PrimaryKeyAttributes.Add(property);
            }

            return property;
        }

        public Property CreateBasicProperty(string name, Type type, Type genericType = null)
        {
            var isGeneric = genericType != null;

            TypeReference cSharpType;
            if (isGeneric)
            {
                var generic = new TypeReference(genericType, true);
                cSharpType = new TypeReference(type, true, true, new List<TypeReference> { generic });
            }
            else
            {
                cSharpType = new TypeReference(type, true);
            }

            var property = new Property(
                name,
                cSharpType,
                this.typesRepository,
                true,
                "public",
                false);

            return property;
        }

        private string GetDefaultValueString(XElement xDefaultValue)
        {
            var defaultValueType = this.xmiWrapper.GetXElementType(xDefaultValue);

            switch (defaultValueType)
            {
                case XElementType.LiteralString:
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