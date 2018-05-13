namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Models;

    public interface IModelClassTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration extendedCodeTypeDeclaration);
    }
}