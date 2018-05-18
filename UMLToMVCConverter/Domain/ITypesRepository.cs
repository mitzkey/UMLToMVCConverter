namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface ITypesRepository
    {
        ExtendedCodeTypeDeclaration GetTypeByXmiId(string xmiId);
        void Add(ExtendedCodeTypeDeclaration type);
        ExtendedCodeTypeDeclaration GetTypeByName(string name);
        IEnumerable<ExtendedCodeTypeDeclaration> GetEnums();
        IEnumerable<ExtendedCodeTypeDeclaration> GetAllTypes();
    }
}