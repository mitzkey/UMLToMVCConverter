﻿namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

    public class NavigationalPropertiesGenerator : INavigationalPropertiesGenerator
    {
        private readonly IPropertyGenerator propertyGenerator;

        public NavigationalPropertiesGenerator(IPropertyGenerator propertyGenerator)
        {
            this.propertyGenerator = propertyGenerator;
        }

        public void Generate(List<Aggregation> aggregations)
        {
            foreach (var aggregation in aggregations)
            {
                var dependentTypeNavigationalProperty = this.propertyGenerator.Generate(aggregation.DependentType, aggregation.DependentTypeAssociationXAttribute);

                dependentTypeNavigationalProperty.IsVirtual = true;

                aggregation.DependentType.Members.Add(dependentTypeNavigationalProperty);

                if (aggregation.DependentTypeMultiplicity == Multiplicity.ExactlyOne
                    || aggregation.DependentTypeMultiplicity == Multiplicity.ZeroOrOne)
                {
                    var principalTypeNavigationalProperty = this.propertyGenerator.Generate(aggregation.PrincipalType, aggregation.PrincipalTypeAssociationXAttribute);

                    principalTypeNavigationalProperty.IsVirtual = true;

                    aggregation.PrincipalType.Members.Add(principalTypeNavigationalProperty);
                }
            }
        }
    }
}