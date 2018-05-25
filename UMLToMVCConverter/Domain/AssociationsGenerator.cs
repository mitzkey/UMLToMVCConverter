namespace UMLToMVCConverter.Domain
{
    using System.Xml.Linq;
    using UMLToMVCConverter.XmiTools;

    public class AssociationsGenerator : IAssociationsGenerator
    {
        private readonly IAssociationsRepository associationsRepository;
        private readonly IAssociationFactory associationFactory;
        private readonly IXmiWrapper xmiWrapper;

        public AssociationsGenerator(IAssociationsRepository associationsRepository, IAssociationFactory associationFactory, IXmiWrapper xmiWrapper)
        {
            this.associationsRepository = associationsRepository;
            this.associationFactory = associationFactory;
            this.xmiWrapper = xmiWrapper;
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
    }
}