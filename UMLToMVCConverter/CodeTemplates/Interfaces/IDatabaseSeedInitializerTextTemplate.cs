namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<Enumeration> enumerations);
    }
}