namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Interfaces;

    public partial class MigrationsManagerClassTextTemplate : IMigrationsManagerClassTextTemplate
    {
        private readonly IMvcProject mvcProject;

        public MigrationsManagerClassTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
