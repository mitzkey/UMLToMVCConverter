namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface ITypesRepository
    {
        TypeModel GetTypeByXmiId(string xmiId);
        void Add(TypeModel type);
        TypeModel GetTypeByName(string name);
        IEnumerable<TypeModel> GetEnums();
        IEnumerable<TypeModel> GetAllTypes();
    }
}