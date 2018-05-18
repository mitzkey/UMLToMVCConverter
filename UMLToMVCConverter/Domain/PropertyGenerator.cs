namespace UMLToMVCConverter.Domain
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.UMLHelpers;
    using UMLToMVCConverter.XmiTools;

    public class PropertyGenerator : IPropertyGenerator
    {
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IXAttributeNameResolver xAttributeNameResolver;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public PropertyGenerator(IUmlTypesHelper umlTypesHelper, IXAttributeNameResolver xAttributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper, IXmiWrapper xmiWrapper, ITypesRepository typesRepository)
        {
            this.umlTypesHelper = umlTypesHelper;
            this.xAttributeNameResolver = xAttributeNameResolver;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
        }

        public Property Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute)
        {
            Insist.IsNotNull(xAttribute, nameof(xAttribute));

            var propertyName = this.xAttributeNameResolver.GetName(xAttribute);

            var cSharpType = this.umlTypesHelper.GetXElementCsharpType(xAttribute);
            var typeReference = ExtendedCodeTypeReference.CreateForType(cSharpType);

            

            var umlVisibility = xAttribute.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharpString(umlVisibility);
            var visibility = cSharpVisibility;

            var isStatic = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isStatic"));

            bool hasSet = true;
            var xIsReadonly = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isReadOnly"));
            if (xIsReadonly)
            {
                hasSet = false;
            }

            int? defaultValueKey = null;
            string defaultValueString = null;
            var xDefaultValue = xAttribute.Element("defaultValue");
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

                    var extendedType = typeReference;
                    if (extendedType.IsGeneric)
                    {
                        throw new NotSupportedException("No default value for generic types supported");
                    }

                    defaultValueString = this.GetDefaultValueString(xDefaultValue);
                }
            }

            var isDerived = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isDerived"));
            if (isDerived)
            {
                hasSet = false;
            }

            var isID = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isID"));

            var property = new Property(
                propertyName,
                typeReference,
                this.typesRepository,
                hasSet,
                visibility,
                isStatic,
                defaultValueKey,
                defaultValueString,
                isDerived,
                isID);

            if (isID)
            {
                type.PrimaryKeyAttributes.Add(property);
            }

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

        public Property GenerateBasicProperty(string name, Type type, Type genericType = null)
        {
            var isGeneric = genericType != null;

            ExtendedType cSharpType;
            if (isGeneric)
            {
                var generic = new ExtendedType(genericType, true);
                cSharpType = new ExtendedType(type, true, true, new List<ExtendedType> {generic});
            }
            else
            {
                cSharpType = new ExtendedType(type, true);
            }

            var propertyType = ExtendedCodeTypeReference.CreateForType(cSharpType);
            var property = new Property(
                name,
                propertyType,
                this.typesRepository,
                true,
                "public",
                false);

            return property;
        }
    }
}