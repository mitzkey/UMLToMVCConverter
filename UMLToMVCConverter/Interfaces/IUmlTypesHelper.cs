namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IUmlTypesHelper
    {
        List<ExtendedCodeTypeDeclaration> CodeTypeDeclarations { get; set; }
        ExtendedType GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
        bool IsEnum(XElement type);
    }
}