namespace UMLToMVCConverter.XmiTools
{
    using System.Xml.Linq;

    public interface IXAttributeNameResolver
    {
        string GetName(XElement attribute);
    }
}