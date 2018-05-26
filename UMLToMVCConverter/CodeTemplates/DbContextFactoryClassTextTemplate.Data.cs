namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.CodeTemplates.Interfaces;
    using UMLToMVCConverter.Domain.Models;

    public partial class DbContextFactoryClassTextTemplate : IDbContextFactoryClassTextTemplate
    {
        private readonly MvcProject mvcProject;

        public DbContextFactoryClassTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
