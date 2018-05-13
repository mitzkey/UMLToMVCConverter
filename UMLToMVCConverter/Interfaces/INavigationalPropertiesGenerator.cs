namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface INavigationalPropertiesGenerator
    {
        void Generate(List<Aggregation> aggregations);
    }
}