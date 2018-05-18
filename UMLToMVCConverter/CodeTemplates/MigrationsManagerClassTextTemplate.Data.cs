namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain.Models;

    public partial class MigrationsManagerClassTextTemplate : IMigrationsManagerClassTextTemplate
    {
        private readonly MvcProject mvcProject;

        public MigrationsManagerClassTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
