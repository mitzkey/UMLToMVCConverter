﻿namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class Association
    {
        public string XmiID { get; }

        public List<AssociationEndMember> Members { get; }

        public RelationshipMultiplicity Multiplicity => this.CalculateRelationshipMultiplicity(this.Members);

        public bool IsGeneratedByConverter => this.XmiID == null;

        public Association(IEnumerable<AssociationEndMember> associationEndMembers, string xmiID)
        {
            this.XmiID = xmiID;
            this.Members = associationEndMembers.ToList();
        }

        private RelationshipMultiplicity CalculateRelationshipMultiplicity(List<AssociationEndMember> members)
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