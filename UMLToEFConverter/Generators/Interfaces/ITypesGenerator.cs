namespace UMLToEFConverter.Generators.Interfaces
{
    using System.Xml.Linq;

    public interface ITypesGenerator
    {
        void Generate(XElement xUmlModel);
    }
}