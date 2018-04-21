namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.ExtendedTypes;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration codeTypeDeclaration);
    }
}