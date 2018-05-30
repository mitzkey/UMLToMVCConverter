namespace UMLToMVCConverter.Generators.Interfaces
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IDataModelGenerator
    {
        DataModel Create(XElement xUmlModel);
    }
}