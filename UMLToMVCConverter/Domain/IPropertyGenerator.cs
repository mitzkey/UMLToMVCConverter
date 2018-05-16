namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Xml.Linq;

    public interface IPropertyGenerator
    {
        ExtendedCodeMemberProperty Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute);

        ExtendedCodeMemberProperty GenerateBasicProperty(string name, Type type, Type genericType = null);
    }
}