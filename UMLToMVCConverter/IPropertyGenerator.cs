namespace UMLToMVCConverter
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IPropertyGenerator
    {
        ExtendedCodeMemberProperty Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute);

        ExtendedCodeMemberProperty GenerateBasicProperty(string name, Type type, Type genericType = null);
    }
}