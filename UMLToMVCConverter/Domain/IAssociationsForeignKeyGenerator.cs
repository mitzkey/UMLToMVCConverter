namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

    public interface IAssociationsForeignKeyGenerator
    {
        void Generate(IEnumerable<Association> associations);
    }
}