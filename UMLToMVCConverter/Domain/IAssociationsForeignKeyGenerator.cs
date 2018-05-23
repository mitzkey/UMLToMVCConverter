namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

    public interface IAssociationsForeignKeyGenerator
    {
        void GenerateForOneToOneAssociations(IEnumerable<Association> oneToOneAssociations);
        void GenerateForOneToManyAssociations(IEnumerable<Association> oneToManyAssociations);
    }
}