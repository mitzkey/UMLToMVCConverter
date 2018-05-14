namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public class ForeignKeysGenerator : IForeignKeysGenerator
    {
        private readonly IPropertyGenerator propertyGenerator;

        public ForeignKeysGenerator(IPropertyGenerator propertyGenerator)
        {
            this.propertyGenerator = propertyGenerator;
        }

        public void Generate(IEnumerable<Aggregation> aggregations)
        {
            foreach (var aggregation in aggregations)
            {
                var compositeType = aggregation.PrincipalType;
                var composedType = aggregation.DependentType;

                if (compositeType.PrimaryKeyAttributes.Count > 0)
                {
                    foreach (var compositeTypePrimaryKeyAttribute in compositeType.PrimaryKeyAttributes)
                    {
                        composedType.ForeignKeys.Add(
                            compositeType.Name + compositeTypePrimaryKeyAttribute.Name,
                            compositeTypePrimaryKeyAttribute);
                    }
                }
                else
                {
                    var foreignKeyName = compositeType.Name + "ID";

                    var foreignKeyProperty = this.propertyGenerator.GenerateBasicProperty(foreignKeyName, typeof(Nullable), typeof(int));

                    composedType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);
                }
            }
        }
    }
}