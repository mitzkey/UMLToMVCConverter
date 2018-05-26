namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AssociationsForeignKeyGenerator : IAssociationsForeignKeyGenerator
    {
        private readonly IForeignKeysGenerator foreignKeyGenerator;
        private readonly INavigationalPropertiesGenerator navigationalPropertiesGenerator;

        public AssociationsForeignKeyGenerator(IForeignKeysGenerator foreignKeyGenerator, INavigationalPropertiesGenerator navigationalPropertiesGenerator)
        {
            this.foreignKeyGenerator = foreignKeyGenerator;
            this.navigationalPropertiesGenerator = navigationalPropertiesGenerator;
        }

        public void Generate(IEnumerable<Association> associations)
        {
            foreach (var association in associations)
            {
                if (association.Multiplicity != RelationshipMultiplicity.OneToOne
                    && association.Multiplicity != RelationshipMultiplicity.OneToMany)
                {
                    throw new ArgumentException("Incorrect association multiplicity");
                }


                var principalMember = this.GetDistinguishedMembers(association, out var dependentMember);
                var foreignKeyGenerated = false;
                if (principalMember.Navigable)
                {
                    this.navigationalPropertiesGenerator.Generate(principalMember, dependentMember);
                    if (principalMember.Multiplicity == Multiplicity.ExactlyOne
                            || principalMember.Multiplicity == Multiplicity.ZeroOrOne)
                    {
                        this.foreignKeyGenerator.Generate(principalMember, dependentMember);
                        foreignKeyGenerated = true;
                    }
                }

                if (dependentMember.Navigable)
                {
                    this.navigationalPropertiesGenerator.Generate(dependentMember, principalMember);
                    if (!foreignKeyGenerated
                        && (dependentMember.Multiplicity == Multiplicity.ExactlyOne
                            || dependentMember.Multiplicity == Multiplicity.ZeroOrOne))
                    {
                        this.foreignKeyGenerator.Generate(dependentMember, principalMember);
                    }
                }
            }
        }

        private AssociationEndMember GetDistinguishedMembers(Association association, out AssociationEndMember dependentMember)
        {
            var members = association.Members;

            var memberWithAggregationTypeDeclaration = members
                .SingleOrDefault(x => x.AggregationKind != AggregationKind.None);

            if (memberWithAggregationTypeDeclaration != null)
            {
                dependentMember = memberWithAggregationTypeDeclaration;
                return members.Single(x => !x.Equals(memberWithAggregationTypeDeclaration));
            }

            var memberWithMultiplicityOne = members.FirstOrDefault(x => x.Multiplicity == Multiplicity.ExactlyOne);
            if (memberWithMultiplicityOne != null)
            {
                dependentMember = members.Single(x => !x.Equals(memberWithMultiplicityOne));
                return memberWithMultiplicityOne;
            }

            var memberWithMultiplicityZeroOrOne =
                members.FirstOrDefault(x => x.Multiplicity == Multiplicity.ZeroOrOne);
            if (memberWithMultiplicityZeroOrOne != null)
            {
                dependentMember = members.Single(x => !x.Equals(memberWithMultiplicityZeroOrOne));
                return memberWithMultiplicityZeroOrOne;
            }

            dependentMember = members.FirstOrDefault();
            var dependentMemberLocal = dependentMember;
            return members.Single(x => !x.Equals(dependentMemberLocal));
        }

    }
}