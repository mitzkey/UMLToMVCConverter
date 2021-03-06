﻿namespace UMLToEFConverter.Generators
{
    using System.Collections.Generic;
    using System.Linq;
    using UMLToEFConverter.Generators.Interfaces;
    using UMLToEFConverter.Models;

    public class EFRelationshipModelGenerator : IEFRelationshipModelGenerator
    {
        public IEnumerable<EFRelationship> CreateRelationshipsConfiguratingOnDeleteBehaviour(IEnumerable<Association> associations)
        {
            var models = new List<EFRelationship>();

            var associationsToConfigure = associations
                .Where(a => a.Members.Any(m => m.Multiplicity == Multiplicity.ExactlyOne));

            foreach (var association in associationsToConfigure)
            {
                var deleteBehavior = "Restrict";
                if (association.AssociationKind == AssociationKind.Composition)
                {
                    if (association.Members.First().Type != association.Members.Last().Type)
                    {
                        deleteBehavior = "Cascade";
                    }
                }

                var targetMember = association.Members.First(m => m.Multiplicity == Multiplicity.ExactlyOne);
                var sourceMember = association.Members.Single(m => !m.Equals(targetMember));

                var sourceMemberMultiplicity = new EFRelationshipMemberMultiplicity(sourceMember.Multiplicity);
                var targetMemberMultiplicity = new EFRelationshipMemberMultiplicity(targetMember.Multiplicity);

                models.Add(new EFRelationship
                {
                    DeleteBehavior = deleteBehavior,
                    SourceMemberMultiplicity = sourceMemberMultiplicity,
                    TargetMemberMultiplicity = targetMemberMultiplicity,
                    SourceTypeName = targetMember.Type.Name,
                    TargetNavigationalPropertyName = targetMember.Name,
                    SourceNavigationalPropertyName = sourceMember.Name,
                    IsTargetMemberNavigable = targetMember.Navigable,
                    IsSourceMemberNavigable = sourceMember.Navigable
                });
            }

            return models;
        }
    }
}