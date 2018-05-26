namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using UMLToMVCConverter.Domain.Models;

    public interface IModelClassTextTemplate
    {
        string TransformText(TypeModel typeModel);
    }
}