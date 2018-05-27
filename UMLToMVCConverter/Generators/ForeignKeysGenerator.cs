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

            if (destinationType.PrimaryKeyAttributes.Count > 0)
            {
                var foreignKeyNames = new List<string>();
                foreach (var destinationTypePrimaryKeyAttribute in destinationType.PrimaryKeyAttributes)
                {
                    var foreignKeyName = sourceMember.Name + destinationTypePrimaryKeyAttribute.Name;
                    var foreignKeyProperty = destinationTypePrimaryKeyAttribute;
                    sourceType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);
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

                sourceType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);

                var navigationalProperty = sourceType.Properties.Single(x => x.Name == sourceMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ foreignKeyName }");
                navigationalProperty.Attributes.Add(attribute);
            }
        }
    }
}