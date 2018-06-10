namespace UMLToEFConverter.CodeTemplates
{
    using UMLToEFConverter.CodeTemplates.Interfaces;
    using UMLToEFConverter.Models;

    public partial class MigrationsManagerClassTextTemplate : IMigrationsManagerClassTextTemplate
    {
        private readonly MvcProject mvcProject;

        public MigrationsManagerClassTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
