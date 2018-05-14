namespace UMLToMVCConverter.Models
{
    using System.Linq;
    using System.Xml.Linq;

    public class DataModelFactory : IDataModelFactory
    {
        private readonly ITypesGenerator typesGenerator;
        private readonly IAggregationsFactory aggregationsFactory;
        private readonly IForeignKeysGenerator foreignKeysGenerator;
        private readonly IEFRelationshipModelFactory efRelationshipModelFactory;
        private readonly INavigationalPropertiesGenerator nagivationalPropertiesGenerator;
        private readonly IEnumerationModelsFactory enumerationModelsFactory;
        private readonly ITypesRepository typesRepository;

        public DataModelFactory(ITypesGenerator typesGenerator, IAggregationsFactory aggregationsFactory, IForeignKeysGenerator foreignKeysGenerator, IEFRelationshipModelFactory efRelationshipModelFactory, INavigationalPropertiesGenerator nagivationalPropertiesGenerator, IEnumerationModelsFactory enumerationModelsFactory, ITypesRepository typesRepository)
        {
            this.typesGenerator = typesGenerator;
            this.aggregationsFactory = aggregationsFactory;
            this.foreignKeysGenerator = foreignKeysGenerator;
            this.efRelationshipModelFactory = efRelationshipModelFactory;
            this.nagivationalPropertiesGenerator = nagivationalPropertiesGenerator;
            this.enumerationModelsFactory = enumerationModelsFactory;
            this.typesRepository = typesRepository;
        }

        public DataModel Create(XElement xUmlModel)
        {
            this.typesGenerator.Generate(xUmlModel);

            var aggregations = this.aggregationsFactory.Create(xUmlModel).ToList();

            this.nagivationalPropertiesGenerator.Generate(aggregations);

            this.foreignKeysGenerator.Generate(aggregations);

            var efRelationshipModels = this.efRelationshipModelFactory.Create(aggregations);

            var enumerationModels = this.enumerationModelsFactory.Create();

            return new DataModel
            {
                Types = this.typesRepository.GetAllTypes(),
                EFRelationshipModels = efRelationshipModels,
                EnumerationModels = enumerationModels
            };
        }
    }
}