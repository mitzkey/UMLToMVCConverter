namespace UMLToEFConverter.Generators
{
    using System.Collections.Generic;
    using UMLToEFConverter.Generators.Interfaces;
    using UMLToEFConverter.Models;
    using UMLToEFConverter.Models.Repositories.Interfaces;
    using UMLToEFConverter.UMLHelpers;

    public class NavigationalPropertiesGenerator : INavigationalPropertiesGenerator
    {
        private readonly ITypesRepository typesRepository;

        public NavigationalPropertiesGenerator(ITypesRepository typesRepository)
        {
            this.typesRepository = typesRepository;
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

            if (destinationMember.Navigable)
            {
                propertyBuilder.WithAttribute(new Attribute("InverseProperty", destinationMember.Name));
            }

            if (sourceMemberPropertyTypeRefernce.IsReferencingXmiDeclaredType)
            {
                var referencingType = this.typesRepository.GetTypeByXmiId(sourceMemberPropertyTypeRefernce.ReferenceTypeXmiID);
                if (referencingType.IsEnum)
                {
                    propertyBuilder.IsReferencingEnumType(true);
                }
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