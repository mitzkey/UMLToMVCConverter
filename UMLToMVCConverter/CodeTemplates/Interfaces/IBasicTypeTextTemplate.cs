namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using UMLToMVCConverter.Models;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(TypeModel type);
    }
}