namespace UMLToMVCConverter.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IEnumerationModelsGenerator
    {
        IEnumerable<Enumeration> Create();
    }
}