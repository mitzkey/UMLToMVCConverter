namespace UMLToEFConverter.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToEFConverter.Models;

    public interface IEnumerationModelsGenerator
    {
        IEnumerable<Enumeration> Create();
    }
}