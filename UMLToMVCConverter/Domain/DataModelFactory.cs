namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Models;

    public class DataModelFactory : IDataModelFactory
    {
        private readonly ITypesGenerator typesGenerator;
        private readonly IAggregationsFactory aggregationsFactory;
        private readonly IForeignKeysGenerator foreignKeysGenerator;
        private readonly IEFRelationshipModelFactory efRelationshipModelFactory;
        private readonly INavigationalPropertiesGenerator nagivationalPropertiesGenerator;
        private readonly IEnumerationModelsFactory enumerationModelsFactory;
        private readonly ITypesRepository typesRepository;
        private readonly IAssociationsForeignKeyGenerator associationsForeignKeyGenerator;
        private readonly IAssociationsRepository associationsRepository;

        public DataModelFactory(
            ITypesGenerator typesGenerator,
            IAggregationsFactory aggregationsFactory,
            IForeignKeysGenerator foreignKeysGenerator,
            IEFRelationshipModelFactory efRelationshipModelFactory,
            INavigationalPropertiesGenerator nagivationalPropertiesGenerator,
            IEnumerationModelsFactory enumerationModelsFactory,
            ITypesRepository typesRepository,
            IAssociationsForeignKeyGenerator associationsForeignKeyGenerator,
            IAssociationsRepository associationsRepository)
        {
            this.typesGenerator = typesGenerator;
            this.aggregationsFactory = aggregationsFactory;
            this.foreignKeysGenerator = foreignKeysGenerator;
            this.efRelationshipModelFactory = efRelationshipModelFactory;
            this.nagivationalPropertiesGenerator = nagivationalPropertiesGenerator;
            this.enumerationModelsFactory = enumerationModelsFactory;
            this.typesRepository = typesRepository;
            this.associationsForeignKeyGenerator = associationsForeignKeyGenerator;
            this.associationsRepository = associationsRepository;
        }

        public DataModel Create(XElement xUmlModel)
        {
            this.typesGenerator.Generate(xUmlModel);

            this.typesGenerator.GenerateManyToManyAssociationTypes();

            // var aggregations = this.aggregationsFactory.Create(xUmlModel).ToList();

            // this.nagivationalPropertiesGenerator.Generate(aggregations);

            // this.foreignKeysGenerator.Generate(aggregations);

            // var efRelationshipModels = this.efRelationshipModelFactory.Create(aggregations);

            var enumerationModels = this.enumerationModelsFactory.Create();

            var oneToOneAssociations = this.associationsRepository.GetAllAssociations()
                .Where(x => x.Multiplicity == RelationshipMultiplicity.OneToOne);
            this.associationsForeignKeyGenerator.GenerateForOneToOneAssociations(oneToOneAssociations);

            var oneToManyAssociations = this.associationsRepository.GetAllAssociations()
                .Where(x => x.Multiplicity == RelationshipMultiplicity.OneToMany);
            this.associationsForeignKeyGenerator.GenerateForOneToManyAssociations(oneToManyAssociations);

            return new DataModel(
                this.typesRepository.GetAllTypes(),
                new List<EFRelationship>(), 
                enumerationModels);
        }
    }
}