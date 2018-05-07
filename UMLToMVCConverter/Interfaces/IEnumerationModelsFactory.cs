namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IEnumerationModelsFactory
    {
        IEnumerable<EnumerationModel> Create(List<ExtendedCodeTypeDeclaration> types);
    }
}