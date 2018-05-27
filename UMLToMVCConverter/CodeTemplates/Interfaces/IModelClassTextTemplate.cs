namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using UMLToMVCConverter.Models;

    public interface IModelClassTextTemplate
    {
        string TransformText(TypeModel typeModel);
    }
}