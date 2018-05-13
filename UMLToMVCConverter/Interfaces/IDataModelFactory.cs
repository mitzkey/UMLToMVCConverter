namespace UMLToMVCConverter.Interfaces
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IDataModelFactory
    {
        DataModel Create(XElement xUmlModel);
    }
}