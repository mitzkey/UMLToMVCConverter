namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<EnumerationModel> Create();
    }
}