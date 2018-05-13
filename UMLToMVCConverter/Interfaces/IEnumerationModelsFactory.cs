namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<EnumerationModel> Create();
    }
}