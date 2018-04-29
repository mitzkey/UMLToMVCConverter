namespace UMLToMVCConverter
{
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Interfaces;

    public class DataModelFactory : IDataModelFactory
    {
        private readonly ITypesFactory typesFactory;
        private readonly IAggregationsFactory aggregationsFactory;
        private readonly IForeignKeysGenerator foreignKeysGenerator;
        private readonly IEFRelationshipModelFactory efRelationshipModelFactory;
        private readonly INavigationalPropertiesGenerator nagivationalPropertiesGenerator;

        public DataModelFactory(ITypesFactory typesFactory, IAggregationsFactory aggregationsFactory, IForeignKeysGenerator foreignKeysGenerator, IEFRelationshipModelFactory efRelationshipModelFactory, INavigationalPropertiesGenerator nagivationalPropertiesGenerator)
        {
            this.typesFactory = typesFactory;
            this.aggregationsFactory = aggregationsFactory;
            this.foreignKeysGenerator = foreignKeysGenerator;
            this.efRelationshipModelFactory = efRelationshipModelFactory;
            this.nagivationalPropertiesGenerator = nagivationalPropertiesGenerator;
        }

        public DataModel Create(XElement xUmlModel)
        {
            var types = this.typesFactory.Create(xUmlModel).ToList();

            var aggregations = this.aggregationsFactory.Create(xUmlModel, types).ToList();

            this.nagivationalPropertiesGenerator.Generate(aggregations);

            this.foreignKeysGenerator.Generate(aggregations);

            var efRelationshipModels = this.efRelationshipModelFactory.Create(aggregations);

            return new DataModel(types, efRelationshipModels);
        }
    }
}