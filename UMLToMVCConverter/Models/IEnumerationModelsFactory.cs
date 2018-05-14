namespace UMLToMVCConverter.Models
{
    using System.Collections.Generic;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<EnumerationModel> Create();
    }
}