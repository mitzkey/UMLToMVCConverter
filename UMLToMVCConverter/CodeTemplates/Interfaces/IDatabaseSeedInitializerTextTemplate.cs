namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IDatabaseSeedInitializerTextTemplate
    {
        string TransformText(IEnumerable<Enumeration> enumerations);
    }
}