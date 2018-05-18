namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;

    public interface IModelClassTextTemplate
    {
        string TransformText(TypeModel typeModel);
    }
}