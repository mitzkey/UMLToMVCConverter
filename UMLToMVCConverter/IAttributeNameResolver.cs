namespace UMLToMVCConverter
{
    using System.Xml.Linq;

    public interface IAttributeNameResolver
    {
        string GetName(XElement attribute);
    }
}