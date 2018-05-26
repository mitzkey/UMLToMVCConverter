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

                if (!dependentMember.Navigable && !principalMember.Navigable)
                {
                    var lowerMultiplicityMember = principalMember.Multiplicity == Multiplicity.ExactlyOne ||
                                                  principalMember.Multiplicity == Multiplicity.ZeroOrOne
                                                    ? principalMember
                                                    : dependentMember;
                    var oppositeMember = this.GetOppositeMember(lowerMultiplicityMember, association);
                    this.navigationalPropertiesGenerator.Generate(lowerMultiplicityMember, oppositeMember);
                    this.foreignKeyGenerator.Generate(lowerMultiplicityMember, oppositeMember);
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
                return this.GetOppositeMember(memberWithAggregationTypeDeclaration, association);
            }

            var memberWithMultiplicityOne = members.FirstOrDefault(x => x.Multiplicity == Multiplicity.ExactlyOne);
            if (memberWithMultiplicityOne != null)
            {
                dependentMember = this.GetOppositeMember(memberWithMultiplicityOne, association);
                return memberWithMultiplicityOne;
            }

            var memberWithMultiplicityZeroOrOne =
                members.FirstOrDefault(x => x.Multiplicity == Multiplicity.ZeroOrOne);
            if (memberWithMultiplicityZeroOrOne != null)
            {
                dependentMember = this.GetOppositeMember(memberWithMultiplicityZeroOrOne, association);
                return memberWithMultiplicityZeroOrOne;
            }

            dependentMember = members.FirstOrDefault();
            var dependentMemberLocal = dependentMember;
            return this.GetOppositeMember(dependentMemberLocal, association);
        }

        private AssociationEndMember GetOppositeMember(AssociationEndMember associationEndMember, Association association)
        {
            return association.Members.Single(x => !x.Equals(associationEndMember));
        }

    }
}