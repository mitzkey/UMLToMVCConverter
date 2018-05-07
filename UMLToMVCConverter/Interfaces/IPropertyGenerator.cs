namespace UMLToMVCConverter.Interfaces
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IPropertyGenerator
    {
        ExtendedCodeMemberProperty Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute);

        ExtendedCodeMemberProperty GenerateBasicProperty(string name, Type type, Type genericType = null);
    }
}