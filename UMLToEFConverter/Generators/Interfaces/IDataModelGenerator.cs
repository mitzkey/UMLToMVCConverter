namespace UMLToEFConverter.Generators.Interfaces
{
    using System.Xml.Linq;
    using UMLToEFConverter.Models;

    public interface IDataModelGenerator
    {
        DataModel Create(XElement xUmlModel);
    }
}