using System.Xml.Linq;
using UMLToMVCConverter.ExtendedTypes;

namespace UMLToMVCConverter
{
    using System.Collections.Generic;

    public interface IUmlTypesHelper
    {
        List<ExtendedCodeTypeDeclaration> CodeTypeDeclarations { get; set; }
        ExtendedType GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
    }
}