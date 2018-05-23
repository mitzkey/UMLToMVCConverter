namespace UMLToMVCConverter.Domain
{
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.UMLHelpers;

    public class NavigationalPropertiesGenerator : INavigationalPropertiesGenerator
    {
        private readonly IPropertyFactory propertyFactory;

        public NavigationalPropertiesGenerator(IPropertyFactory propertyFactory)
        {
            this.propertyFactory = propertyFactory;
        }

        public void Generate(AssociationEndMember dependentMember, AssociationEndMember principalMember)
        {
            var dependentMemberPropertyTypeRefernce = TypeReference.Builder()
                .IsBaseType(true)
                .SetName(principalMember.Type.Name)
                .Build();
            var dependentTypeNavigationalProperty = Property.Builder()
                .SetName(dependentMember.Name)
                .SetTypeReference(dependentMemberPropertyTypeRefernce)
                .HasSet(true)
                .SetVisibility(CSharpVisibilityString.Public)
                .IsVirtual(true)
                .Build();

            dependentMember.Type.Properties.Add(dependentTypeNavigationalProperty);

            if (dependentMember.Multiplicity == Multiplicity.ExactlyOne
                || dependentMember.Multiplicity == Multiplicity.ZeroOrOne)
            {
                var principalMemberPropertyTypeRefernce = TypeReference.Builder()
                    .IsBaseType(true)
                    .SetName(dependentMember.Type.Name)
                    .Build();
                var principalTypeNavigationalProperty = Property.Builder()
                    .SetName(principalMember.Name)
                    .SetTypeReference(principalMemberPropertyTypeRefernce)
                    .HasSet(true)
                    .SetVisibility(CSharpVisibilityString.Public)
                    .IsVirtual(true)
                    .Build();

                principalMember.Type.Properties.Add(principalTypeNavigationalProperty);
            }
        }
    }
}