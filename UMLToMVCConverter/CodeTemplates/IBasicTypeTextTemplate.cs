namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration codeTypeDeclaration);
    }
}