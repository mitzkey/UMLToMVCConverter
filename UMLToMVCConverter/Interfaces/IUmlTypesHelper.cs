namespace UMLToMVCConverter.Interfaces
{
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IUmlTypesHelper
    {
        ExtendedType GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
        bool IsEnum(XElement type);
    }
}