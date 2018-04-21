namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.ExtendedTypes;

    public partial class ModelClassTextTemplate : IModelClassTextTemplate
    {
        private readonly IMvcProject mvcProject;
        private readonly IBasicTypeTextTemplate basicTypeTextTemplate;
        ExtendedCodeTypeDeclaration codeTypeDeclaration;

        public ModelClassTextTemplate(IMvcProject mvcProject, IBasicTypeTextTemplate basicTypeTextTemplate)
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
