namespace UMLToMVCConverter.CodeTemplates
{
    using Autofac;
    using UMLToMVCConverter.Domain.Models;

    public partial class BasicTypeTextTemplate : IBasicTypeTextTemplate
    {
        private readonly MvcProject mvcProject;
        private readonly IComponentContext componentContext;
        ExtendedCodeTypeDeclaration codeTypeDeclaration;

        public BasicTypeTextTemplate(MvcProject mvcProject, IComponentContext componentContext)
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
