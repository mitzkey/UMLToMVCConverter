namespace UMLToMVCConverter.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IAssociationsForeignKeyGenerator
    {
        void Generate(IEnumerable<Association> associations);
    }
}