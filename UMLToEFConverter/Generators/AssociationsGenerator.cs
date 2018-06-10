namespace UMLToEFConverter.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using UMLToEFConverter.Common.XmiTools.Interfaces;
    using UMLToEFConverter.Generators.Deserializers.Interfaces;
    using UMLToEFConverter.Generators.Interfaces;
    using UMLToEFConverter.Models;
    using UMLToEFConverter.Models.Repositories.Interfaces;
    using UMLToEFConverter.UMLHelpers;

    public class AssociationsGenerator : IAssociationsGenerator
    {
        private readonly IAssociationsRepository associationsRepository;
        private readonly IAssociationDeserializer associationDeserializer;
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public AssociationsGenerator(
            IAssociationsRepository associationsRepository,
            IAssociationDeserializer associationDeserializer,
            IXmiWrapper xmiWrapper,
            ITypesRepository typesRepository)
        {
            this.associationsRepository = associationsRepository;
            this.associationDeserializer = associationDeserializer;
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
        }

        public void Generate(XElement umlModel)
        {
            var xAssociations = this.xmiWrapper.GetXAssociations(umlModel);
            foreach (var xAssociation in xAssociations)
            {
                var association = this.associationDeserializer.Create(xAssociation);

                this.associationsRepository.Add(association);
            }
        }

        public void GenerateManyToManyAssociationTypes()
        {
            foreach (var association in this.associationsRepository
                .GetAllAssociations()
                .Where(x => x.Multiplicity == RelationshipMultiplicity.ManyToMany).ToList())
            {
                var associationTypeNameBuilder = new StringBuilder();
                association.Members.ForEach(x => associationTypeNameBuilder.Append(x.Name));
                var associationTypeName = associationTypeNameBuilder.ToString();
                var type = new TypeModel(associationTypeName, true, CSharpVisibilityString.Public);
                this.typesRepository.Add(type);

                foreach (var member in association.Members)
                {
                    var oppositeMember = association.Members.Single(x => !x.Equals(member));
                    var reducedMultiplicity = ReduceMultiplicity(member.Multiplicity);
                    var associationTypeMember = new AssociationEndMember(null, oppositeMember.Name, reducedMultiplicity, oppositeMember.AssociationKind, type, oppositeMember.Navigable);
                    var parentAssociationMemberTypesNewMember = new AssociationEndMember(null, member.Name, oppositeMember.Multiplicity, member.AssociationKind, member.Type, member.Navigable);
                    var childAssociationMembers = new List<AssociationEndMember> { associationTypeMember, parentAssociationMemberTypesNewMember };
                    var childAssociation = new Association(childAssociationMembers, null, null);
                    this.associationsRepository.Add(childAssociation);
                }
            }
        }

        public void GenerateForAssociationClasses()
        {
            var associationsWithAssociationClasses = this.associationsRepository.GetAllAssociations()
                .Where(a => a.HasAssociationClass).ToList();
            foreach (var association in associationsWithAssociationClasses)
            {
                var associationClass = association.AssociationClass;
                foreach (var associationEndMember in association.Members)
                {
                    var parentAssociationMemberNewMember = 
                        new AssociationEndMember(null, associationEndMember.Type.Name, Multiplicity.ExactlyOne, AssociationKind.None, associationClass, true);
                    var associationClassEndMember = 
                        new AssociationEndMember(null, associationEndMember.Name, associationEndMember.Multiplicity, AssociationKind.None, associationEndMember.Type, true);
                    var childAssociationMembers =
                        new List<AssociationEndMember> {associationClassEndMember, parentAssociationMemberNewMember};
                    var childAssociation = new Association(childAssociationMembers, null, null);
                    this.associationsRepository.Add(childAssociation);
                }

                this.associationsRepository.Remove(association);
            }
        }

        private static Multiplicity ReduceMultiplicity(Multiplicity multiplicity)
        {
            if (multiplicity != Multiplicity.OneOrMore
                && multiplicity != Multiplicity.ZeroOrMore)
            {
                throw new ArgumentException("Can't reduce provided multiplicity");
            }

            return multiplicity == Multiplicity.ZeroOrMore
                ? Multiplicity.ZeroOrOne
                : Multiplicity.ExactlyOne;
        }
    }
}