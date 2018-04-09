using System.Xml.Linq;
using UMLToMVCConverter.ExtendedTypes;

namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;

    public interface IUmlTypesHelper
    {
        List<CodeTypeDeclaration> CodeTypeDeclarations { get; set; }
        ExtendedType GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
    }
}