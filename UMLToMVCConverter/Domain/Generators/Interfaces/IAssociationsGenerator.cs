namespace UMLToMVCConverter.Domain.Generators.Interfaces
{
    using System.Xml.Linq;

    public interface IAssociationsGenerator
    {
        void Generate(XElement umlModel);
        void GenerateManyToManyAssociationTypes();
    }
}