namespace UMLToEFConverter.Models.Repositories.Interfaces
{
    using System.Collections.Generic;

    public interface ITypesRepository
    {
        TypeModel GetTypeByXmiId(string xmiId);
        void Add(TypeModel type);
        TypeModel GetTypeByName(string name);
        IEnumerable<TypeModel> GetEnums();
        IEnumerable<TypeModel> GetAllTypes();
        void DeclareType(TypeModel typeDeclaration);
        TypeModel GetTypeDeclaration(string xTypeName);
        bool TryGetTypeByXmiId(string xmiID, out TypeModel typeModel);
    }
}