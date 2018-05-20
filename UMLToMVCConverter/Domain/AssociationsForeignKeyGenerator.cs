namespace UMLToMVCConverter.Domain
{
    using System.Linq;

    public class AssociationsForeignKeyGenerator : IAssociationsForeignKeyGenerator
    {
        private readonly IAssociationsRepository associationsRepository;
        private readonly IForeignKeysGenerator foreignKeyGenerator;

        public AssociationsForeignKeyGenerator(IForeignKeysGenerator foreignKeyGenerator, IAssociationsRepository associationsRepository)
        {
            this.foreignKeyGenerator = foreignKeyGenerator;
            this.associationsRepository = associationsRepository;
        }

        public void Generate()
        {
            var oneToOneAssociations = this.associationsRepository.GetAllAssociations()
                .Where(x => x.Multiplicity == RelationshipMultiplicity.OneToOne);

            foreach (var association in oneToOneAssociations)
            {
                var dependentMember = this.GetDistinguishedMembers(association, out var principalMember);

                this.foreignKeyGenerator.Generate(dependentMember, principalMember);
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
            
            principalMember = members.FirstOrDefault();
            var principalMemberLocal = principalMember;
            return members.Single(x => !x.Equals(principalMemberLocal));
        }
    }
}