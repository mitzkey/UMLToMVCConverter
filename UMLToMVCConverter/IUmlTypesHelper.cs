using System.Xml.Linq;
using UMLToMVCConverter.ExtendedTypes;

namespace UMLToMVCConverter
{
    public interface IUmlTypesHelper
    {
        ExtendedType GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
    }
}