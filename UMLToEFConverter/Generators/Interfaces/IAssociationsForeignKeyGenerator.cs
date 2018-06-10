namespace UMLToEFConverter.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToEFConverter.Models;

    public interface IAssociationsForeignKeyGenerator
    {
        void Generate(IEnumerable<Association> associations);
    }
}