namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.ExtendedTypes;

    public interface IModelClassTextTemplate
    {
        string TransformText(ExtendedCodeTypeDeclaration extendedCodeTypeDeclaration);
    }
}