namespace UMLToMVCConverter.Domain.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Generators.Interfaces;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.Domain.Repositories.Interfaces;
    using UMLToMVCConverter.UMLHelpers;
    using UMLToMVCConverter.XmiTools.Interfaces;

    public class AssociationsGenerator : IAssociationsGenerator
    {
        private readonly IAssociationsRepository associationsRepository;
        private readonly IAssociationFactory associationFactory;
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public AssociationsGenerator(IAssociationsRepository associationsRepository, IAssociationFactory associationFactory, IXmiWrapper xmiWrapper, ITypesRepository typesRepository)
        {
            this.associationsRepository = associationsRepository;
            this.associationFactory = associationFactory;
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
        }

        public void Generate(XElement umlModel)
        {
            var xAssociations = this.xmiWrapper.GetXAssociations(umlModel);
            foreach (var xAssociation in xAssociations)
            {
                var association = this.associationFactory.Create(xAssociation);

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
                    var childAssociation = new Association(childAssociationMembers, null);
                    this.associationsRepository.Add(childAssociation);
                }
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