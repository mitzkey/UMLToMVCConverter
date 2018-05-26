namespace UMLToMVCConverter.Domain.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IAssociationsForeignKeyGenerator
    {
        void Generate(IEnumerable<Association> associations);
    }
}