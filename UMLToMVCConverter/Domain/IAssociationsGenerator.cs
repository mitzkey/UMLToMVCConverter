namespace UMLToMVCConverter.Domain
{
    using System.Xml.Linq;

    public interface IAssociationsGenerator
    {
        void Generate(XElement umlModel);
    }
}