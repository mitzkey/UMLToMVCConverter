namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public class ForeignKeysGenerator : IForeignKeysGenerator
    {
        private readonly IPropertyFactory propertyFactory;

        public ForeignKeysGenerator(IPropertyFactory propertyFactory)
        {
            this.propertyFactory = propertyFactory;
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

                    var foreignKeyProperty = this.propertyFactory.CreateBasicProperty(foreignKeyName, typeof(Nullable), typeof(int));

                    composedType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);
                }
            }
        }
    }
}