﻿namespace UMLToMVCConverter.Domain
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
        private readonly IAssociationsGenerator associationsGenerator;

        public DataModelFactory(
            ITypesGenerator typesGenerator,
            IAggregationsFactory aggregationsFactory,
            IForeignKeysGenerator foreignKeysGenerator,
            IEFRelationshipModelFactory efRelationshipModelFactory,
            INavigationalPropertiesGenerator nagivationalPropertiesGenerator,
            IEnumerationModelsFactory enumerationModelsFactory,
            ITypesRepository typesRepository,
            IAssociationsForeignKeyGenerator associationsForeignKeyGenerator,
            IAssociationsRepository associationsRepository,
            IAssociationsGenerator associationsGenerator)
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
            this.associationsGenerator = associationsGenerator;
        }

        public DataModel Create(XElement xUmlModel)
        {
            this.typesGenerator.Generate(xUmlModel);

            this.associationsGenerator.Generate(xUmlModel);

            this.typesGenerator.GenerateManyToManyAssociationTypes();

            // var aggregations = this.aggregationsFactory.Create(xUmlModel).ToList();

            // this.nagivationalPropertiesGenerator.Generate(aggregations);

            // this.foreignKeysGenerator.Generate(aggregations);

            // var efRelationshipModels = this.efRelationshipModelFactory.Create(aggregations);

            var enumerationModels = this.enumerationModelsFactory.Create();

            var associationsToGeneratePropertiesFor = this.associationsRepository.GetAllAssociations()
                .Where(x => x.Multiplicity != RelationshipMultiplicity.ManyToMany);
            this.associationsForeignKeyGenerator.Generate(associationsToGeneratePropertiesFor);

            return new DataModel(
                this.typesRepository.GetAllTypes(),
                new List<EFRelationship>(), 
                enumerationModels);
        }
    }
}