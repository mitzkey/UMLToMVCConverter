namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public partial class DatabaseSeedInitializerTextTemplate : IDatabaseSeedInitializerTextTemplate
    {
        private IEnumerable<Enumeration> enumerations;
        private readonly MvcProject mvcProject;

        public DatabaseSeedInitializerTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }

        public string TransformText(IEnumerable<Enumeration> enumerations)
        {
            this.enumerations = enumerations;

            return this.TransformText();
        }
    }
}
