namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public partial class DatabaseSeedInitializerTextTemplate : IDatabaseSeedInitializerTextTemplate
    {
        private IEnumerable<EnumerationModel> enumerations;
        private readonly IMvcProject mvcProject;

        public DatabaseSeedInitializerTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }

        public string TransformText(IEnumerable<EnumerationModel> enumerations)
        {
            this.enumerations = enumerations;

            return this.TransformText();
        }
    }
}
