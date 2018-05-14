namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IForeignKeysGenerator
    {
        void Generate(IEnumerable<Aggregation> aggregations);
    }
}