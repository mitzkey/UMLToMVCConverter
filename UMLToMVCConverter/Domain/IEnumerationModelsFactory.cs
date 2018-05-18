namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<Enumeration> Create();
    }
}