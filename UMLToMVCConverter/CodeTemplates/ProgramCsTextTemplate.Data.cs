﻿namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain;

    public partial class ProgramCsTextTemplate : IProgramCsTextTemplate
    {
        private readonly IMvcProject mvcProject;

        public ProgramCsTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }
    }
}
