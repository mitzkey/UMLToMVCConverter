namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IPropertyGenerator
    {
        ExtendedCodeMemberProperty Generate(ExtendedCodeTypeDeclaration type, XElement xAttribute);
    }
}