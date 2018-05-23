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

        public void GenerateForOneToOneAssociations(IEnumerable<Association> oneToOneAssociations)
        {
            foreach (var association in oneToOneAssociations)
            {
                if (association.Multiplicity != RelationshipMultiplicity.OneToOne)
                {
                    throw new ArgumentException("Incorrect association multiplicity");
                }
                var dependentMember = GetDistinguishedMembers(association, out var principalMember);

                this.foreignKeyGenerator.Generate(dependentMember, principalMember);
            }
        }

        public void GenerateForOneToManyAssociations(IEnumerable<Association> oneToManyAssociations)
        {
            foreach (var association in oneToManyAssociations)
            {
                if (association.Multiplicity != RelationshipMultiplicity.OneToMany)
                {
                    throw new ArgumentException("Incorrect association multiplicity");
                }

                if (association.IsGeneratedByConverter)
                {
                    this.navigationalPropertiesGenerator.Generate(association.Members.First(), association.Members.Last());
                    this.navigationalPropertiesGenerator.Generate(association.Members.Last(), association.Members.First());

                    this.foreignKeyGenerator.Generate(association.Members.First(), association.Members.Last());
                    this.foreignKeyGenerator.Generate(association.Members.Last(), association.Members.First());
                }
            }
        }

        private AssociationEndMember GetDistinguishedMembers(Association association, out AssociationEndMember principalMember)
        {
            var members = association.Members;

            var memberWithAggregationTypeDeclaration = members
                .SingleOrDefault(x => x.AggregationKind != AggregationKind.None);

            if (memberWithAggregationTypeDeclaration != null)
            {
                principalMember = memberWithAggregationTypeDeclaration;
                return members.Single(x => !x.Equals(memberWithAggregationTypeDeclaration));
            }

            var memberWithMultiplicityOne = members.FirstOrDefault(x => x.Multiplicity == Multiplicity.ExactlyOne);
            if (memberWithMultiplicityOne != null)
            {
                principalMember = memberWithMultiplicityOne;
                return members.Single(x => !x.Equals(memberWithMultiplicityOne));
            }

            var memberWithMultiplicityZeroOrOne =
                members.FirstOrDefault(x => x.Multiplicity == Multiplicity.ZeroOrOne);
            if (memberWithMultiplicityZeroOrOne != null)
            {
                principalMember = memberWithMultiplicityZeroOrOne;
                return members.Single(x => !x.Equals(memberWithMultiplicityZeroOrOne));
            }
            
            principalMember = members.FirstOrDefault();
            var principalMemberLocal = principalMember;
            return members.Single(x => !x.Equals(principalMemberLocal));
        }
    }
}