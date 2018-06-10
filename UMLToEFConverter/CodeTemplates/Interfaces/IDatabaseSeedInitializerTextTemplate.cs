namespace UMLToEFConverter.CodeTemplates.Interfaces
{
    using System.Collections.Generic;
    using UMLToEFConverter.Models;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<Enumeration> enumerations);
    }
}