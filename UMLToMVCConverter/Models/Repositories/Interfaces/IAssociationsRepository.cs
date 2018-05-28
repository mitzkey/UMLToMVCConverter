namespace UMLToMVCConverter.Models.Repositories.Interfaces
{
    using System.Collections.Generic;

    public interface IAssociationsRepository
    {
        IEnumerable<Association> GetAllAssociations();

        void Add(Association association);
        void Remove(Association association);
    }
}