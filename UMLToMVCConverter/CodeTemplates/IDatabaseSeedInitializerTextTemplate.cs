namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<EnumerationModel> enumerations);
    }
}