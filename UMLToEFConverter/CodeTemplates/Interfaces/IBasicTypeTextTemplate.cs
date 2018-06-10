namespace UMLToEFConverter.CodeTemplates.Interfaces
{
    using UMLToEFConverter.Models;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(TypeModel type);
    }
}