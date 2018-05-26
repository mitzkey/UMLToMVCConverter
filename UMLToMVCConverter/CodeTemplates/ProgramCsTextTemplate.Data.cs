namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.CodeTemplates.Interfaces;
    using UMLToMVCConverter.Domain.Models;

    public partial class ProgramCsTextTemplate : IProgramCsTextTemplate
    {
        private readonly MvcProject mvcProject;

        public ProgramCsTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
