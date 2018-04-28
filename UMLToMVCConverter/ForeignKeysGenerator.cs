namespace UMLToMVCConverter
{
    using System.Collections.Generic;

    public class ForeignKeysGenerator : IForeignKeysGenerator
    {
        public void Generate(IEnumerable<Aggregation> aggregations)
        {
            foreach (var aggregation in aggregations)
            {
                var compositeType = aggregation.CompositeType;
                var composedType = aggregation.ComposedType;

                foreach (var compositeTypePrimaryKeyAttribute in compositeType.PrimaryKeyAttributes)
                {
                    composedType.ForeignKeys.Add(compositeType.Name + compositeTypePrimaryKeyAttribute.Name, compositeTypePrimaryKeyAttribute);
                }
            }
        }
    }
}