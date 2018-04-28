namespace UMLToMVCConverter
{
    using System.Collections.Generic;

    public interface IForeignKeysGenerator
    {
        void Generate(IEnumerable<Aggregation> aggregations);
    }
}