namespace UMLToMVCConverter
{
    using System.Collections.Generic;

    public interface INavigationalPropertiesGenerator
    {
        void Generate(List<Aggregation> aggregations);
    }
}