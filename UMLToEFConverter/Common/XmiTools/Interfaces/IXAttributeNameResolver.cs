namespace UMLToEFConverter.Common.XmiTools.Interfaces
{
    using System.Xml.Linq;

    public interface IXAttributeNameResolver
    {
        string GetName(XElement attribute);
        string GetAssociationsEndName(XElement associationEndXElement);
    }
}