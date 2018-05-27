namespace UMLToMVCConverter.Domain.Repositories.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IAssociationsRepository
    {
        IEnumerable<Association> GetAllAssociations();

        void Add(Association association);
        void Remove(Association association);
    }
}