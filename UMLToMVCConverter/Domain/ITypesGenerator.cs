namespace UMLToMVCConverter.Domain
{
    using System.Xml.Linq;

    public interface ITypesGenerator
    {
        void Generate(XElement xUmlModel);

        void GenerateManyToManyAssociationTypes();
    }
}