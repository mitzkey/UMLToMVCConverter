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

    public class AssociationsFactory : IAssociationsFactory
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IRelationshipFactory relationshipFactory;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IAttributeNameResolver attributeNameResolver;

        public AssociationsFactory(IXmiWrapper xmiWrapper, IRelationshipFactory relationshipFactory, IUmlVisibilityMapper umlVisibilityMapper, IUmlTypesHelper umlTypesHelper, IAttributeNameResolver attributeNameResolver)
        {
            this.xmiWrapper = xmiWrapper;
            this.relationshipFactory = relationshipFactory;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.umlTypesHelper = umlTypesHelper;
            this.attributeNameResolver = attributeNameResolver;
        }

        public IEnumerable<IRelationship> Create(XElement xUmlModel, IEnumerable<ExtendedCodeTypeDeclaration> types)
        {
            var relationships = new List<IRelationship>();

            var xAssociations = this.xmiWrapper.GetXAssociations(xUmlModel);

            var typesList = types.ToList();

            foreach (var xAssociation in xAssociations)
            {
                var associationEnds = this.xmiWrapper.GetAssociationEnds(xAssociation);

                var aggregationKind = associationEnds.Item1.OptionalAttributeValue("aggregation")
                                      ?? associationEnds.Item2.OptionalAttributeValue("aggregation");
                
                if (aggregationKind == "composite")
                {
                    var ownerTypeAssociationProperty =
                        string.IsNullOrWhiteSpace(associationEnds.Item1.OptionalAttributeValue("aggregation"))
                            ? associationEnds.Item2
                            : associationEnds.Item1;

                    this.AddCompositionNavigationalProperty(ownerTypeAssociationProperty, typesList);

                    var ownedTypeAssociationProperty = associationEnds.Item1.Equals(ownerTypeAssociationProperty)
                        ? associationEnds.Item2
                        : associationEnds.Item1;

                    this.AddCompositionNavigationalProperty(ownedTypeAssociationProperty, typesList);

                    var ownerTypeId = this.xmiWrapper.GetElementsId(ownerTypeAssociationProperty.Parent);

                    var ownerType = typesList.Single(x => x.XmiID == ownerTypeId);

                    var ownedTypeId = this.xmiWrapper.GetElementsId(ownedTypeAssociationProperty.Parent);

                    var ownedType = typesList.Single(x => x.XmiID == ownedTypeId);

                    foreach (var ownersID in ownerType.IDs)
                    {
                        ownedType.ForeignKeys.Add(ownerType.Name + ownersID.Name, ownersID);
                    }
                }

                var relationship = this.relationshipFactory.Create(xAssociation, typesList);
                relationships.Add(relationship);
            }

            return relationships;
        }

        private void AddCompositionNavigationalProperty(XElement associationProperty, IEnumerable<ExtendedCodeTypeDeclaration> types)
        {
            var typeId = this.xmiWrapper.GetElementsId(associationProperty.Parent);

            var type = types.Single(x => x.XmiID == typeId);

            var navigationalProperty = this.GenerateAttribute(type, associationProperty);

            navigationalProperty.IsVirtual = true;

            type.Members.Add(navigationalProperty);
        }

        private ExtendedCodeMemberProperty GenerateAttribute(ExtendedCodeTypeDeclaration codeTypeDeclaration, XElement attribute)
        {
            Insist.IsNotNull(attribute, nameof(attribute));

            //type                
            var cSharpType = this.umlTypesHelper.GetXElementCsharpType(attribute);
            CodeTypeReference typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

            //declaration
            var codeMemberProperty = new ExtendedCodeMemberProperty
            {
                Type = typeRef,
                Name = this.attributeNameResolver.GetName(attribute),
                HasSet = true
            };

            var umlVisibility = attribute.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharp(umlVisibility);
            codeMemberProperty.Attributes = codeMemberProperty.Attributes | cSharpVisibility;

            var isStatic = Convert.ToBoolean(attribute.OptionalAttributeValue("isStatic"));
            if (isStatic)
            {
                codeMemberProperty.Attributes = codeMemberProperty.Attributes | MemberAttributes.Static;
            }

            var xIsReadonly = Convert.ToBoolean(attribute.OptionalAttributeValue("isReadOnly"));
            if (xIsReadonly)
            {
                codeMemberProperty.HasSet = false;
            }

            var xDefaultValue = attribute.Element("defaultValue");
            if (xDefaultValue != null)
            {
                var extendedType = (ExtendedCodeTypeReference)codeMemberProperty.Type;
                if (extendedType.IsGeneric || extendedType.IsNamedType)
                {
                    throw new NotSupportedException("No default value for generic or declared named types supported");
                }

                codeMemberProperty.DefaultValueString = xDefaultValue.ObligatoryAttributeValue("value");
            }

            var xIsDerived = Convert.ToBoolean(attribute.OptionalAttributeValue("isDerived"));
            if (xIsDerived)
            {
                codeMemberProperty.HasSet = false;
                codeMemberProperty.IsDerived = true;
            }

            var xIsID = Convert.ToBoolean(attribute.OptionalAttributeValue("isID"));
            if (xIsID)
            {
                codeMemberProperty.IsID = true;
                codeTypeDeclaration.IDs.Add(codeMemberProperty);
            }

            return codeMemberProperty;
        }
    }
}