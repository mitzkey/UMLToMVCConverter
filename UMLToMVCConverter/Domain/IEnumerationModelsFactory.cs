namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<EnumerationModel> Create();
    }
}