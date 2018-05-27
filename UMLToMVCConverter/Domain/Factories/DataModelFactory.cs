namespace UMLToMVCConverter.Domain.Factories
{
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Generators.Interfaces;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.Domain.Repositories.Interfaces;

    public class DataModelFactory : IDataModelFactory
    {
        private readonly ITypesGenerator typesGenerator;
        private readonly IEFRelationshipModelFactory efRelationshipModelFactory;
        private readonly IEnumerationModelsFactory enumerationModelsFactory;
        private readonly ITypesRepository typesRepository;
        private readonly IAssociationsForeignKeyGenerator associationsForeignKeyGenerator;
        private readonly IAssociationsRepository associationsRepository;
        private readonly IAssociationsGenerator associationsGenerator;

        public DataModelFactory(
            ITypesGenerator typesGenerator,
            IEFRelationshipModelFactory efRelationshipModelFactory,
            IEnumerationModelsFactory enumerationModelsFactory,
            ITypesRepository typesRepository,
            IAssociationsForeignKeyGenerator associationsForeignKeyGenerator,
            IAssociationsRepository associationsRepository,
            IAssociationsGenerator associationsGenerator)
        {
            this.typesGenerator = typesGenerator;
            this.efRelationshipModelFactory = efRelationshipModelFactory;
            this.enumerationModelsFactory = enumerationModelsFactory;
            this.typesRepository = typesRepository;
            this.associationsForeignKeyGenerator = associationsForeignKeyGenerator;
            this.associationsRepository = associationsRepository;
            this.associationsGenerator = associationsGenerator;
        }

        public DataModel Create(XElement xUmlModel)
        {
            this.typesGenerator.Generate(xUmlModel);

            this.associationsGenerator.Generate(xUmlModel);

            this.associationsGenerator.GenerateForAssociationClasses();

            this.associationsGenerator.GenerateManyToManyAssociationTypes();

            var enumerationModels = this.enumerationModelsFactory.Create();

            var allAssociations = this.associationsRepository.GetAllAssociations().ToList();
            var associationsToGeneratePropertiesFor = allAssociations
                .Where(x => x.Multiplicity != RelationshipMultiplicity.ManyToMany);
            this.associationsForeignKeyGenerator.Generate(associationsToGeneratePropertiesFor);

            var efRelationships =
                this.efRelationshipModelFactory.CreateRelationshipsConfiguratingOnDeleteBehaviour(allAssociations);

            return new DataModel(
                this.typesRepository.GetAllTypes(),
                efRelationships, 
                enumerationModels);
        }
    }
}