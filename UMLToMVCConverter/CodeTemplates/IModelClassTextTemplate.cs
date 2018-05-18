namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;

    public interface IModelClassTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration extendedCodeTypeDeclaration);
    }
}