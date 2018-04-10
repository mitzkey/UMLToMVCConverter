namespace UMLToMVCConverter.CodeTemplates
{
    public partial class DbContextFactoryClassTextTemplate : IDbContextFactoryClassTextTemplate
    {
        private readonly IMvcProject mvcProject;

        public DbContextFactoryClassTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
