namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;

    public interface IForeignKeysGenerator
    {
        void Generate(IEnumerable<Aggregation> aggregations);
    }
}