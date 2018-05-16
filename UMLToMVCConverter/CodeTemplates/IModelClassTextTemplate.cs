namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain;

    public interface IModelClassTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration extendedCodeTypeDeclaration);
    }
}