namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Models;

    public interface IPropertyGenerator
    {
        Property Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute);

        Property GenerateBasicProperty(string name, Type type, Type genericType = null);
    }
}