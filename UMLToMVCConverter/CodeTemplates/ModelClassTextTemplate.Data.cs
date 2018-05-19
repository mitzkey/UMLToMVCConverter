namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain.Models;

    public partial class ModelClassTextTemplate : IModelClassTextTemplate
    {
        private readonly MvcProject mvcProject;
        private readonly IBasicTypeTextTemplate basicTypeTextTemplate;
        TypeModel type;

        public ModelClassTextTemplate(MvcProject mvcProject, IBasicTypeTextTemplate basicTypeTextTemplate)
        {
            this.mvcProject = mvcProject;
            this.basicTypeTextTemplate = basicTypeTextTemplate;
        }

        public string TransformText(TypeModel typeModel)
        {
            this.type = typeModel;
            return this.TransformText();
        }
    }
}
