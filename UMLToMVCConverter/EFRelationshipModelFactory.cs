namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;

    public class EFRelationshipModelFactory : IEFRelationshipModelFactory
    {
        private readonly IXmiWrapper xmiWrapper;

        public EFRelationshipModelFactory(IXmiWrapper xmiWrapper)
        {
            this.xmiWrapper = xmiWrapper;
        }

        public EFRelationshipModel Create(XElement xAssociation, IEnumerable<ExtendedCodeTypeDeclaration> types)
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

                var ownedTypeAssociationProperty = associationEnds.Item1.Equals(ownerTypeAssociationProperty)
                    ? associationEnds.Item2
                    : associationEnds.Item1;

                var ownedTypeId = this.xmiWrapper.GetElementsId(ownedTypeAssociationProperty.Parent);

                var ownedType = types.Single(x => x.XmiID == ownedTypeId);

                var ownerTypeName = ownerTypeAssociationProperty.Parent.ObligatoryAttributeValue("name");
                var ownedTypeName = ownedTypeAssociationProperty.Parent.ObligatoryAttributeValue("name");
                var foreignKeyPropertyNames = ownedType.ForeignKeys.Keys;
                var multiplicity = new RelationshipMultiplicity
                {
                    Name = "One",
                    IsObligatory = false
                };

                return new EFRelationshipModel(foreignKeyPropertyNames)
                {
                    DeleteBehavior = "Cascade",
                    Multiplicity = multiplicity,
                    SourceEntityName = ownerTypeName,
                    TargetEntityName = ownedTypeName,
                };
            }

            return null;
        }
    }
}