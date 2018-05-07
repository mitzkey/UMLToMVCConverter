namespace UMLToMVCConverter
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;
    using UMLToMVCConverter.Mappers;

    public class PropertyGenerator : IPropertyGenerator
    {
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IAttributeNameResolver attributeNameResolver;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public PropertyGenerator(IUmlTypesHelper umlTypesHelper, IAttributeNameResolver attributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper, IXmiWrapper xmiWrapper, ITypesRepository typesRepository)
        {
            this.umlTypesHelper = umlTypesHelper;
            this.attributeNameResolver = attributeNameResolver;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
        }

        public ExtendedCodeMemberProperty Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute)
        {
            Insist.IsNotNull(xAttribute, nameof(xAttribute));

            var cSharpType = this.umlTypesHelper.GetXElementCsharpType(xAttribute);
            var typeReference = ExtendedCodeTypeReference.CreateForType(cSharpType);

            var property = new ExtendedCodeMemberProperty
            {
                Type = typeReference,
                Name = this.attributeNameResolver.GetName(xAttribute),
                HasSet = true
            };

            var umlVisibility = xAttribute.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharp(umlVisibility);
            property.Attributes = property.Attributes | cSharpVisibility;

            var isStatic = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isStatic"));
            if (isStatic)
            {
                property.Attributes = property.Attributes | MemberAttributes.Static;
            }

            var xIsReadonly = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isReadOnly"));
            if (xIsReadonly)
            {
                property.HasSet = false;
            }

            var xDefaultValue = xAttribute.Element("defaultValue");
            if (xDefaultValue != null)
            {
                var extendedType = (ExtendedCodeTypeReference)property.Type;
                if (extendedType.IsGeneric)
                {
                    throw new NotSupportedException("No default value for generic types supported");
                }

                property.DefaultValueString = this.GetDefaultValueString(xDefaultValue);
            }

            var xIsDerived = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isDerived"));
            if (xIsDerived)
            {
                property.HasSet = false;
                property.IsDerived = true;
            }

            var xIsID = Convert.ToBoolean(xAttribute.OptionalAttributeValue("isID"));
            if (xIsID)
            {
                property.IsID = true;
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

        public ExtendedCodeMemberProperty GenerateBasicProperty(string name, Type type, Type genericType = null)
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
            var property = new ExtendedCodeMemberProperty
            {
                Type = propertyType,
                Name = name,
                HasSet = true
            };

            return property;
        }
    }
}