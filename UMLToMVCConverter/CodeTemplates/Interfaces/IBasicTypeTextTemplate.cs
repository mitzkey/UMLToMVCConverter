namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using UMLToMVCConverter.Domain.Models;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(TypeModel type);
    }
}