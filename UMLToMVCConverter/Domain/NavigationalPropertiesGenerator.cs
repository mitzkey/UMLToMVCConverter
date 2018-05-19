namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public class NavigationalPropertiesGenerator : INavigationalPropertiesGenerator
    {
        private readonly IPropertyFactory propertyFactory;

        public NavigationalPropertiesGenerator(IPropertyFactory propertyFactory)
        {
            this.propertyFactory = propertyFactory;
        }

        public void Generate(List<Aggregation> aggregations)
        {
            foreach (var aggregation in aggregations)
            {
                var dependentTypeNavigationalProperty = this.propertyFactory.Create(aggregation.DependentType, aggregation.DependentTypeAssociationXAttribute);

                dependentTypeNavigationalProperty.IsVirtual = true;

                aggregation.DependentType.Properties.Add(dependentTypeNavigationalProperty);

                if (aggregation.DependentTypeMultiplicity == Multiplicity.ExactlyOne
                    || aggregation.DependentTypeMultiplicity == Multiplicity.ZeroOrOne)
                {
                    var principalTypeNavigationalProperty = this.propertyFactory.Create(aggregation.PrincipalType, aggregation.PrincipalTypeAssociationXAttribute);

                    principalTypeNavigationalProperty.IsVirtual = true;

                    aggregation.PrincipalType.Properties.Add(principalTypeNavigationalProperty);
                }
            }
        }
    }
}