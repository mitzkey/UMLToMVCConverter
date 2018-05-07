namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<EnumerationModel> enumerations);
    }
}