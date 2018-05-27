namespace UMLToMVCConverter.Generators
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IDataModelGenerator
    {
        DataModel Create(XElement xUmlModel);
    }
}