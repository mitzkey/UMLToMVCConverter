namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.UMLHelpers;

    public class NavigationalPropertiesGenerator : INavigationalPropertiesGenerator
    {
        private readonly IPropertyFactory propertyFactory;

        public NavigationalPropertiesGenerator(IPropertyFactory propertyFactory)
        {
            this.propertyFactory = propertyFactory;
        }

        public void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember)
        {
            var sourceMemberPropertyTypeReferenceBuilder = TypeReference.Builder()
                .IsBaseType(true);

            if (sourceMember.Multiplicity == Multiplicity.OneOrMore
                || sourceMember.Multiplicity == Multiplicity.ZeroOrMore)
            {
                var innerType = TypeReference.Builder()
                    .SetName(destinationMember.Type.Name)
                    .Build();

                sourceMemberPropertyTypeReferenceBuilder
                    .SetType(typeof(ICollection<>))
                    .IsCollection(true)
                    .IsGeneric(true)
                    .SetGeneric(innerType);
            }
            else
            {
                sourceMemberPropertyTypeReferenceBuilder
                    .SetName(destinationMember.Type.Name);
            }

            var sourceMemberPropertyTypeRefernce = sourceMemberPropertyTypeReferenceBuilder.Build();

            var sourceTypeNavigationalProperty = Property.Builder()
                .SetName(sourceMember.Name)
                .SetTypeReference(sourceMemberPropertyTypeRefernce)
                .HasSet(true)
                .SetVisibility(CSharpVisibilityString.Public)
                .IsVirtual(true)
                .Build();

            sourceMember.Type.Properties.Add(sourceTypeNavigationalProperty);
        }
    }
}