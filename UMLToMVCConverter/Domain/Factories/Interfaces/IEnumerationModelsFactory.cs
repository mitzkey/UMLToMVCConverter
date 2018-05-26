namespace UMLToMVCConverter.Domain.Factories.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<Enumeration> Create();
    }
}