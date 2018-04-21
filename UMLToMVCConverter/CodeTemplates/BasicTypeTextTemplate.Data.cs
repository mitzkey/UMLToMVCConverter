namespace UMLToMVCConverter.CodeTemplates
{
    using Autofac;
    using UMLToMVCConverter.ExtendedTypes;

    public partial class BasicTypeTextTemplate : IBasicTypeTextTemplate
    {
        private readonly IMvcProject mvcProject;
        private readonly IComponentContext componentContext;
        ExtendedCodeTypeDeclaration codeTypeDeclaration;

        public BasicTypeTextTemplate(IMvcProject mvcProject, IComponentContext componentContext)
        {
            this.mvcProject = mvcProject;
            this.componentContext = componentContext;
        }

        public string TransformText(ExtendedCodeTypeDeclaration codeTypeDeclaration)
        {
            this.codeTypeDeclaration = codeTypeDeclaration;

            return this.TransformText();
        }
    }
}
