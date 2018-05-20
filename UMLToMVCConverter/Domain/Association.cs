namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class Association
    {
        public List<AssociationEndMember> Members { get; }

        public RelationshipMultiplicity Multiplicity { get; set; }    

        public Association(IEnumerable<AssociationEndMember> associationEndMembers)
        {
            this.Members = associationEndMembers.ToList();
            this.Multiplicity = CalculateRelationshipMultiplicityFromMembers(this.Members);
        }

        private RelationshipMultiplicity CalculateRelationshipMultiplicityFromMembers(List<AssociationEndMember> members)
        {
            var multiplicities = members.Select(x => x.Multiplicity).ToList();

            if (multiplicities.Contains(Domain.Multiplicity.ExactlyOne)
                || multiplicities.Contains(Domain.Multiplicity.ZeroOrOne))
            {
                if (multiplicities.Contains(Domain.Multiplicity.OneOrMore)
                    || multiplicities.Contains(Domain.Multiplicity.ZeroOrMore))
                {
                    return RelationshipMultiplicity.OneToMany;
                }

                return RelationshipMultiplicity.OneToOne;
            }

            return RelationshipMultiplicity.ManyToMany;
        }
    }
}