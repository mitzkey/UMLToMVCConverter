namespace UMLToMVCConverter.Models
{
    using System.Xml.Linq;

    public interface IDataModelFactory
    {
        DataModel Create(XElement xUmlModel);
    }
}