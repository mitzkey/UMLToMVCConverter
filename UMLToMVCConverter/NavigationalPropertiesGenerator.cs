namespace UMLToMVCConverter
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
                if (aggregation.DependentTypeMultiplicity == Multiplicity.ExactlyOne
                    || aggregation.DependentTypeMultiplicity == Multiplicity.ZeroOrOne)
                {
                    var navigationalProperty = this.propertyGenerator.Generate(aggregation.DependentType, aggregation.DependentTypeAssociationXAttribute);

                    navigationalProperty.IsVirtual = true;

                    aggregation.DependentType.Members.Add(navigationalProperty);
                }

                if (aggregation.PrincipalTypeMultiplicity == Multiplicity.ExactlyOne
                    || aggregation.PrincipalTypeMultiplicity== Multiplicity.ZeroOrOne)
                {
                    var navigationalProperty = this.propertyGenerator.Generate(aggregation.PrincipalType, aggregation.PrincipalTypeAssociationXAttribute);

                    navigationalProperty.IsVirtual = true;

                    aggregation.PrincipalType.Members.Add(navigationalProperty);
                }
            }
        }
    }
}