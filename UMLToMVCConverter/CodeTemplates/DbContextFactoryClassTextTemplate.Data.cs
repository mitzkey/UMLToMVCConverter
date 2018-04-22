namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Interfaces;

    public partial class DbContextFactoryClassTextTemplate : IDbContextFactoryClassTextTemplate
    {
        private readonly IMvcProject mvcProject;

        public DbContextFactoryClassTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
