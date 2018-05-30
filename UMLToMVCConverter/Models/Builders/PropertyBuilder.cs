namespace UMLToMVCConverter.Models.Builders
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models.Repositories.Interfaces;

    public class PropertyBuilder
    {
        private string name;
        private TypeReference typeReference;
        private bool isReadOnly;
        private ITypesRepository typesRepository;
        private string visibility;
        private bool isStatic;
        private int? defaultValueKey;
        private string defaultValueString;
        private bool isDerived;
        private bool isID;
        private readonly List<Attribute> attributes;
        private bool isVirtual;
        private bool isReferencingEnumType;

        public PropertyBuilder()
        {
            this.attributes = new List<Attribute>();
        }

        public Property Build()
        {
            return new Property(
                this.name,
                this.typeReference,
                this.isReadOnly,
                this.visibility,
                this.isStatic,
                this.defaultValueKey,
                this.defaultValueString,
                this.isDerived,
                this.isID,
                this.attributes,
                this.isVirtual,
                this.isReferencingEnumType);
        }

        public PropertyBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }

        public PropertyBuilder SetTypeReference(TypeReference typeReference)
        {
            this.typeReference = typeReference;
            return this;
        }

        public PropertyBuilder IsReadOnly(bool isReadOnly)
        {
            this.isReadOnly = isReadOnly;
            return this;
        }

        public PropertyBuilder SetVisibility(string visibility)
        {
            this.visibility = visibility;
            return this;
        }

        public PropertyBuilder IsStatic(bool isStatic)
        {
            this.isStatic = isStatic;
            return this;
        }

        public PropertyBuilder SetDefaultValueKey(int? defaultValueKey)
        {
            this.defaultValueKey = defaultValueKey;
            return this;
        }

        public PropertyBuilder SetDefaultValueString(string defaultValueString)
        {
            this.defaultValueString = defaultValueString;
            return this;
        }

        public PropertyBuilder IsDerived(bool isDerived)
        {
            this.isDerived = isDerived;
            return this;
        }

        public PropertyBuilder IsID(bool isID)
        {
            this.isID = isID;
            return this;
        }

        public PropertyBuilder WithAttribute(Attribute attribute)
        {
            this.attributes.Add(attribute);
            return this;
        }

        public PropertyBuilder IsVirtual(bool isVirtual)
        {
            this.isVirtual = isVirtual;
            return this;
        }

        public PropertyBuilder IsReferencingEnumType(bool isReferencingEnumType)
        {
            this.isReferencingEnumType = isReferencingEnumType;
            return this;
        }
    }
}