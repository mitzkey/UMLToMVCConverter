namespace UMLToEFConverter.CodeTemplates
{
    using UMLToEFConverter.CodeTemplates.Interfaces;
    using UMLToEFConverter.Models;

    public partial class ProgramCsTextTemplate : IProgramCsTextTemplate
    {
        private readonly MvcProject mvcProject;

        public ProgramCsTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
