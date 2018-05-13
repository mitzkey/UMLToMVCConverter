namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<EnumerationModel> enumerations);
    }
}