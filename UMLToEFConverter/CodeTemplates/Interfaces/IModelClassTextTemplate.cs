namespace UMLToEFConverter.CodeTemplates.Interfaces
{
    using UMLToEFConverter.Models;

    public interface IModelClassTextTemplate
    {
        string TransformText(TypeModel typeModel);
    }
}