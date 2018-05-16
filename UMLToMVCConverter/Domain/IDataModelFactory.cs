namespace UMLToMVCConverter.Domain
{
    using System.Xml.Linq;

    public interface IDataModelFactory
    {
        DataModel Create(XElement xUmlModel);
    }
}