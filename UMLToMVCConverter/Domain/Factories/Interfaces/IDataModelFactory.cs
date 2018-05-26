namespace UMLToMVCConverter.Domain.Factories.Interfaces
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Models;

    public interface IDataModelFactory
    {
        DataModel Create(XElement xUmlModel);
    }
}