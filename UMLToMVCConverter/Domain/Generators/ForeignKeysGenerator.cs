namespace UMLToMVCConverter.Domain.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Generators.Interfaces;
    using UMLToMVCConverter.Domain.Models;
    using Attribute = UMLToMVCConverter.Domain.Models.Attribute;

    public class ForeignKeysGenerator : IForeignKeysGenerator
    {
        private readonly IPropertyFactory propertyFactory;

        public ForeignKeysGenerator(IPropertyFactory propertyFactory)
        {
            this.propertyFactory = propertyFactory;
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

                var foreignKeyProperty = this.propertyFactory.CreateBasicProperty(foreignKeyName, typeof(Nullable), typeof(int));

                sourceType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);

                var navigationalProperty = sourceType.Properties.Single(x => x.Name == sourceMember.Name);
                var attribute = new Attribute("ForeignKey", $"{ foreignKeyName }");
                navigationalProperty.Attributes.Add(attribute);
            }
        }
    }
}