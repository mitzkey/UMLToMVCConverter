namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Models;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration codeTypeDeclaration);
    }
}