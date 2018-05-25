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

                foreach (var associationEndMember in association.Members)
                {
                    if (associationEndMember.Navigable)
                    {
                        var oppositeMember = association.Members.Single(x => x != associationEndMember);
                        this.navigationalPropertiesGenerator.Generate(oppositeMember, associationEndMember);
                        this.foreignKeyGenerator.Generate(oppositeMember, associationEndMember);
                    }
                }
            }
        }
    }
}