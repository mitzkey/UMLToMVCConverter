namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Interfaces;

    public class DataModelFactory : IDataModelFactory
    {
        private readonly ITypesFactory typesFactory;
        private readonly IAssociationsFactory associationsFactory;

        public DataModelFactory(ITypesFactory typesFactory, IAssociationsFactory associationsFactory)
        {
            this.typesFactory = typesFactory;
            this.associationsFactory = associationsFactory;
        }

        public DataModel Create(XElement xUmlModel)
        {
            var types = this.typesFactory.Create(xUmlModel);

            var associations = this.associationsFactory.Create(xUmlModel, types);

            return new DataModel(types, associations);
        }
    }
}