namespace UMLToEFConverter.UMLHelpers.Interfaces
{
    using System.Xml.Linq;
    using UMLToEFConverter.Models;

    public interface IUmlTypesHelper
    {
        TypeReference GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
        bool IsEnum(XElement type);
    }
}