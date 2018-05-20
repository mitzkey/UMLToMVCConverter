namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

    public interface IAssociationsRepository
    {
        IEnumerable<Association> GetAllAssociations();
        void Add(Association association);
    }
}