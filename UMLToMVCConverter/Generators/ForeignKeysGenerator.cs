namespace UMLToMVCConverter.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.Deserializers.Interfaces;
    using UMLToMVCConverter.Generators.Interfaces;
    using UMLToMVCConverter.Models;
    using Attribute = UMLToMVCConverter.Models.Attribute;

    public class ForeignKeysGenerator : IForeignKeysGenerator
    {
        private readonly IPropertyDeserializer propertyDeserializer;

        public ForeignKeysGenerator(IPropertyDeserializer propertyDeserializer)
        {
            this.propertyDeserializer = propertyDeserializer;
        }

        public void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember)
        {
            var destinationType = destinationMember.Type;
            var sourceType = sourceMember.Type;

            bool required = sourceMember.Multiplicity == Multiplicity.ExactlyOne
                || sourceMember.Multiplicity == Multiplicity.OneOrMore;

            if (destinationType.PrimaryKeyAttributes.Count > 0)
            {
                var foreignKeyNames = new List<string>();
                foreach (var destinationTypePrimaryKeyAttribute in destinationType.PrimaryKeyAttributes)
                {
                    var foreignKeyName = sourceMember.Name + destinationTypePrimaryKeyAttribute.Name;
                    var foreignKeyProperty = this.propertyDeserializer.CreateBasicProperty(foreignKeyName, destinationTypePrimaryKeyAttribute.TypeReference.Type, destinationTypePrimaryKeyAttribute.TypeReference.Generic?.Type);

                    if (required)
                    {
                        foreignKeyProperty.Attributes.Add(new Attribute("Required", null));
                    }

                    sourceType.Properties.Add(foreignKeyProperty);
                    foreignKeyNames.Add(foreignKeyName);
                }

                var navigationalProperty = sourceType.Properties.Single(x => x.Name == sourceMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ string.Join(",", foreignKeyNames) }");
                navigationalProperty.Attributes.Add(attribute);
            }
            else
            {
                var foreignKeyName = sourceMember.Name + "ID";

                var foreignKeyProperty = this.propertyDeserializer.CreateBasicProperty(foreignKeyName, typeof(Nullable), typeof(int));

                if (required)
                {
                    foreignKeyProperty.Attributes.Add(new Attribute("Required", null));
                }

                sourceType.Properties.Add(foreignKeyProperty);

                var navigationalProperty = sourceType.Properties.Single(x => x.Name == sourceMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ foreignKeyName }");
                navigationalProperty.Attributes.Add(attribute);
            }
        }
    }
}