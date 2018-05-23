namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.Domain.Models;
    using Attribute = UMLToMVCConverter.Domain.Models.Attribute;

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

        public void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember)
        {
            var destinationType = destinationMember.Type;
            var sourceType = sourceMember.Type;

            if (destinationType.PrimaryKeyAttributes.Count > 0)
            {
                foreach (var destinationTypePrimaryKeyAttribute in destinationType.PrimaryKeyAttributes)
                {
                    var foreignKeyName = sourceMember.Name + destinationTypePrimaryKeyAttribute.Name;
                    var foreignKeyProperty = destinationTypePrimaryKeyAttribute;
                    sourceType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);
                }

                var navigationalProperty = sourceType.Properties.Single(x => x.Name == sourceMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ string.Join(",", sourceType.ForeignKeys.Keys) }");
                navigationalProperty.Attributes.Add(attribute);
            }
            else
            {
                var foreignKeyName = sourceMember.Name + "ID";

                var foreignKeyProperty = this.propertyFactory.CreateBasicProperty(foreignKeyName, typeof(Nullable), typeof(int));

                sourceType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);

                var navigationalProperty = sourceType.Properties.Single(x => x.Name == sourceMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ foreignKeyName }");
                navigationalProperty.Attributes.Add(attribute);
            }
        }
    }
}