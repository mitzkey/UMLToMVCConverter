namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<EnumerationModel> enumerations);
    }
}