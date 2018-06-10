namespace UMLToEFConverter.CodeTemplates
{
    using UMLToEFConverter.CodeTemplates.Interfaces;
    using UMLToEFConverter.Models;

    public partial class DbContextFactoryClassTextTemplate : IDbContextFactoryClassTextTemplate
    {
        private readonly MvcProject mvcProject;

        public DbContextFactoryClassTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
