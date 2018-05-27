namespace UMLToMVCConverter.Domain.Generators
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Factories;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Generators.Interfaces;
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

            var propertyBuilder = Property.Builder();

            if (sourceMember.Multiplicity == Multiplicity.ExactlyOne
                || sourceMember.Multiplicity == Multiplicity.OneOrMore)
            {
                propertyBuilder.WithAttribute(new Attribute("Required", null));
            }

            if (destinationMember.Navigable)
            {
                propertyBuilder.WithAttribute(new Attribute("InverseProperty", destinationMember.Name));
            }

            var sourceTypeNavigationalProperty = propertyBuilder
                .SetName(sourceMember.Name)
                .SetTypeReference(sourceMemberPropertyTypeRefernce)
                .SetVisibility(CSharpVisibilityString.Public)
                .IsVirtual(true)
                .Build();

            sourceMember.Type.Properties.Add(sourceTypeNavigationalProperty);
        }
    }
}