namespace UMLToMVCConverter.Domain.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Association
    {
        public string XmiID { get; }

        public List<AssociationEndMember> Members { get; }

        public RelationshipMultiplicity Multiplicity => this.CalculateRelationshipMultiplicity(this.Members);

        public bool IsGeneratedByConverter => this.XmiID == null;

        public AssociationKind AssociationKind => this.Members.Any(m => m.AssociationKind != AssociationKind.None)
            ? this.Members.Where(m => m.AssociationKind != AssociationKind.None).Select(m => m.AssociationKind).Single()
            : AssociationKind.None;

        public bool HasAssociationClass => this.AssociationClass != null;
        public TypeModel AssociationClass { get; }

        public Association(IEnumerable<AssociationEndMember> associationEndMembers, string xmiID, TypeModel associationClass)
        {
            this.XmiID = xmiID;
            this.AssociationClass = associationClass;
            this.Members = associationEndMembers.ToList();
        }

        private RelationshipMultiplicity CalculateRelationshipMultiplicity(List<AssociationEndMember> members)
        {
            var multiplicities = members.Select(x => x.Multiplicity).ToList();

            if (multiplicities.Contains(Models.Multiplicity.ExactlyOne)
                || multiplicities.Contains(Models.Multiplicity.ZeroOrOne))
            {
                if (multiplicities.Contains(Models.Multiplicity.OneOrMore)
                    || multiplicities.Contains(Models.Multiplicity.ZeroOrMore))
                {
                    return RelationshipMultiplicity.OneToMany;
                }

                return RelationshipMultiplicity.OneToOne;
            }

            return RelationshipMultiplicity.ManyToMany;
        }
    }
}