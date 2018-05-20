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

        public void Generate(AssociationEndMember dependentMember, AssociationEndMember principalMember)
        {
            var principalType = principalMember.Type;
            var dependentType = dependentMember.Type;

            if (principalType.PrimaryKeyAttributes.Count > 0)
            {
                foreach (var principalTypePrimaryKeyAttribute in principalType.PrimaryKeyAttributes)
                {
                    var foreignKeyName = dependentMember.Name + principalTypePrimaryKeyAttribute.Name;
                    var foreignKeyProperty = principalTypePrimaryKeyAttribute;
                    dependentType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);
                }

                var navigationalProperty = dependentType.Properties.Single(x => x.Name == dependentMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ string.Join(",", dependentType.ForeignKeys.Keys) }");
                navigationalProperty.Attributes.Add(attribute);
            }
            else
            {
                var foreignKeyName = principalMember.Name + "ID";

                var foreignKeyProperty = this.propertyFactory.CreateBasicProperty(foreignKeyName, typeof(Nullable), typeof(int));

                dependentType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);

                var navigationalProperty = dependentType.Properties.Single(x => x.Name == principalMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ string.Join(",", dependentType.ForeignKeys.Keys) }");
                navigationalProperty.Attributes.Add(attribute);
            }
        }
    }
}