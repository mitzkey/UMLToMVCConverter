namespace UMLToEFConverter.CodeTemplates
{
    using Autofac;
    using UMLToEFConverter.CodeTemplates.Interfaces;
    using UMLToEFConverter.Models;

    public partial class BasicTypeTextTemplate : IBasicTypeTextTemplate
    {
        private readonly MvcProject mvcProject;
        private readonly IComponentContext componentContext;
        private TypeModel type;

        public BasicTypeTextTemplate(MvcProject mvcProject, IComponentContext componentContext)
        {
            this.mvcProject = mvcProject;
            this.componentContext = componentContext;
        }

        public string TransformText(TypeModel type)
        {
            this.type = type;

            return this.TransformText();
        }
    }
}
