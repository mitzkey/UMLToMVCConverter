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

    public class AggregationsFactory : IAggregationsFactory
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IEFRelationshipModelFactory iefRelationshipModelFactory;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IAttributeNameResolver attributeNameResolver;

        public AggregationsFactory(IXmiWrapper xmiWrapper, IEFRelationshipModelFactory iefRelationshipModelFactory, IUmlVisibilityMapper umlVisibilityMapper, IUmlTypesHelper umlTypesHelper, IAttributeNameResolver attributeNameResolver)
        {
            this.xmiWrapper = xmiWrapper;
            this.iefRelationshipModelFactory = iefRelationshipModelFactory;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.umlTypesHelper = umlTypesHelper;
            this.attributeNameResolver = attributeNameResolver;
        }

        public IEnumerable<Aggregation> Create(XElement xUmlModel, IEnumerable<ExtendedCodeTypeDeclaration> types)
        {
            var aggregations = new List<Aggregation>();

            var xAggregations = this.xmiWrapper.GetXAggregations(xUmlModel);

            var typesList = types.ToList();

            foreach (var xAggregation in xAggregations)
            {
                var associationEnds = this.xmiWrapper.GetAssociationEnds(xAggregation);

                var aggregationKind = associationEnds.Item1.OptionalAttributeValue("aggregation")
                                      ?? associationEnds.Item2.OptionalAttributeValue("aggregation");
                
                if (aggregationKind == "composite")
                {
                    var compositeTypeAssociationProperty =
                        string.IsNullOrWhiteSpace(associationEnds.Item1.OptionalAttributeValue("aggregation"))
                            ? associationEnds.Item2
                            : associationEnds.Item1;

                    var composedTypeAssociationProperty = associationEnds.Item1.Equals(compositeTypeAssociationProperty)
                        ? associationEnds.Item2
                        : associationEnds.Item1;

                    var compositeTypeId = this.xmiWrapper.GetElementsId(compositeTypeAssociationProperty.Parent);

                    var compositeType = typesList.Single(x => x.XmiID == compositeTypeId);

                    compositeType.Members.Add(this.CreateCompositionNavigationalProperty(compositeTypeAssociationProperty, compositeType));

                    var composedTypeId = this.xmiWrapper.GetElementsId(composedTypeAssociationProperty.Parent);

                    var composedType = typesList.Single(x => x.XmiID == composedTypeId);

                    composedType.Members.Add(this.CreateCompositionNavigationalProperty(composedTypeAssociationProperty, composedType));

                    aggregations.Add(
                        new Aggregation
                        {
                            AggregationKind = AggregationKinds.Composition,
                            CompositeType = compositeType,
                            ComposedType = composedType
                        });
                }
            }

            return aggregations;
        }

        private ExtendedCodeMemberProperty CreateCompositionNavigationalProperty(XElement aggregationProperty, ExtendedCodeTypeDeclaration type)
        {
            var navigationalProperty = this.GenerateAttribute(type, aggregationProperty);

            navigationalProperty.IsVirtual = true;

            return navigationalProperty;
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
                codeTypeDeclaration.PrimaryKeyAttributes.Add(codeMemberProperty);
            }

            return codeMemberProperty;
        }
    }
}