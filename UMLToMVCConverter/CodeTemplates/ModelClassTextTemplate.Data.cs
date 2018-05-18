namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain.Models;

    public partial class ModelClassTextTemplate : IModelClassTextTemplate
    {
        private readonly MvcProject mvcProject;
        private readonly IBasicTypeTextTemplate basicTypeTextTemplate;
        ExtendedCodeTypeDeclaration codeTypeDeclaration;

        public ModelClassTextTemplate(MvcProject mvcProject, IBasicTypeTextTemplate basicTypeTextTemplate)
        {
            this.mvcProject = mvcProject;
            this.basicTypeTextTemplate = basicTypeTextTemplate;
        }

        public string TransformText(ExtendedCodeTypeDeclaration extendedCodeTypeDeclaration)
        {
            this.codeTypeDeclaration = extendedCodeTypeDeclaration;
            return this.TransformText();
        }
    }
}
