namespace UMLToMVCConverter.Generators
{
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Deserializers.Interfaces;
    using UMLToMVCConverter.Generators.Interfaces;
    using UMLToMVCConverter.Models;
    using UMLToMVCConverter.Repositories.Interfaces;

    public class DataModelGenerator : IDataModelGenerator
    {
        private readonly ITypesGenerator typesGenerator;
        private readonly Domain.Factories.Interfaces.IEFRelationshipModelGenerator iefRelationshipModelGenerator;
        private readonly IEnumerationModelsDeserializer enumerationModelsDeserializer;
        private readonly ITypesRepository typesRepository;
        private readonly IAssociationsForeignKeyGenerator associationsForeignKeyGenerator;
        private readonly IAssociationsRepository associationsRepository;
        private readonly IAssociationsGenerator associationsGenerator;

        public DataModelGenerator(
            ITypesGenerator typesGenerator,
            Domain.Factories.Interfaces.IEFRelationshipModelGenerator iefRelationshipModelGenerator,
            IEnumerationModelsDeserializer enumerationModelsDeserializer,
            ITypesRepository typesRepository,
            IAssociationsForeignKeyGenerator associationsForeignKeyGenerator,
            IAssociationsRepository associationsRepository,
            IAssociationsGenerator associationsGenerator)
        {
            this.typesGenerator = typesGenerator;
            this.iefRelationshipModelGenerator = iefRelationshipModelGenerator;
            this.enumerationModelsDeserializer = enumerationModelsDeserializer;
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

            var enumerationModels = this.enumerationModelsDeserializer.Create();

            var allAssociations = this.associationsRepository.GetAllAssociations().ToList();
            var associationsToGeneratePropertiesFor = allAssociations
                .Where(x => x.Multiplicity != RelationshipMultiplicity.ManyToMany);
            this.associationsForeignKeyGenerator.Generate(associationsToGeneratePropertiesFor);

            var efRelationships =
                this.iefRelationshipModelGenerator.CreateRelationshipsConfiguratingOnDeleteBehaviour(allAssociations);

            return new DataModel(
                this.typesRepository.GetAllTypes(),
                efRelationships, 
                enumerationModels);
        }
    }
}