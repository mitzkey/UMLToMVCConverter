namespace UMLToMVCConverter
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
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

        public PropertyGenerator(IUmlTypesHelper umlTypesHelper, IAttributeNameResolver attributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper)
        {
            this.umlTypesHelper = umlTypesHelper;
            this.attributeNameResolver = attributeNameResolver;
            this.umlVisibilityMapper = umlVisibilityMapper;
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
                if (extendedType.IsGeneric || extendedType.IsNamedType)
                {
                    throw new NotSupportedException("No default value for generic or declared named types supported");
                }

                property.DefaultValueString = xDefaultValue.ObligatoryAttributeValue("value");
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