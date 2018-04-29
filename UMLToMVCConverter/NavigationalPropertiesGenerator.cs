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

    public class NavigationalPropertiesGenerator : INavigationalPropertiesGenerator
    {
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IAttributeNameResolver attributeNameResolver;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;

        public NavigationalPropertiesGenerator(IUmlTypesHelper umlTypesHelper, IAttributeNameResolver attributeNameResolver, IUmlVisibilityMapper umlVisibilityMapper)
        {
            this.umlTypesHelper = umlTypesHelper;
            this.attributeNameResolver = attributeNameResolver;
            this.umlVisibilityMapper = umlVisibilityMapper;
        }

        public void Generate(List<Aggregation> aggregations)
        {
            foreach (var aggregation in aggregations)
            {
                if (aggregation.DependentTypeMultiplicity == Multiplicity.ExactlyOne
                    || aggregation.DependentTypeMultiplicity == Multiplicity.ZeroOrOne)
                {
                    var navigationalProperty = this.GenerateProperty(aggregation.DependentType, aggregation.DependentTypeAssociationXAttribute);

                    navigationalProperty.IsVirtual = true;

                    aggregation.DependentType.Members.Add(navigationalProperty);
                }

                if (aggregation.PrincipalTypeMultiplicity == Multiplicity.ExactlyOne
                    || aggregation.PrincipalTypeMultiplicity== Multiplicity.ZeroOrOne)
                {
                    var navigationalProperty = this.GenerateProperty(aggregation.PrincipalType, aggregation.PrincipalTypeAssociationXAttribute);

                    navigationalProperty.IsVirtual = true;

                    aggregation.PrincipalType.Members.Add(navigationalProperty);
                }
            }
        }

        private ExtendedCodeMemberProperty GenerateProperty(ExtendedCodeTypeDeclaration type, XElement attribute)
        {
            Insist.IsNotNull(attribute, nameof(attribute));

            //type                
            var cSharpType = this.umlTypesHelper.GetXElementCsharpType(attribute);
            CodeTypeReference typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

            //declaration
            var property = new ExtendedCodeMemberProperty
            {
                Type = typeRef,
                Name = this.attributeNameResolver.GetName(attribute),
                HasSet = true
            };

            var umlVisibility = attribute.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharp(umlVisibility);
            property.Attributes = property.Attributes | cSharpVisibility;

            var isStatic = Convert.ToBoolean(attribute.OptionalAttributeValue("isStatic"));
            if (isStatic)
            {
                property.Attributes = property.Attributes | MemberAttributes.Static;
            }

            var xIsReadonly = Convert.ToBoolean(attribute.OptionalAttributeValue("isReadOnly"));
            if (xIsReadonly)
            {
                property.HasSet = false;
            }

            var xDefaultValue = attribute.Element("defaultValue");
            if (xDefaultValue != null)
            {
                var extendedType = (ExtendedCodeTypeReference)property.Type;
                if (extendedType.IsGeneric || extendedType.IsNamedType)
                {
                    throw new NotSupportedException("No default value for generic or declared named types supported");
                }

                property.DefaultValueString = xDefaultValue.ObligatoryAttributeValue("value");
            }

            var xIsDerived = Convert.ToBoolean(attribute.OptionalAttributeValue("isDerived"));
            if (xIsDerived)
            {
                property.HasSet = false;
                property.IsDerived = true;
            }

            var xIsID = Convert.ToBoolean(attribute.OptionalAttributeValue("isID"));
            if (xIsID)
            {
                property.IsID = true;
                type.PrimaryKeyAttributes.Add(property);
            }

            return property;
        }
    }
}