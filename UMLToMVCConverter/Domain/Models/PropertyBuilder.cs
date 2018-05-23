namespace UMLToMVCConverter.Domain.Models
{
    using System.Collections.Generic;

    public class PropertyBuilder
    {
        private string name;
        private TypeReference typeReference;
        private bool hasSet;
        private ITypesRepository typesRepository;
        private string visibility;
        private bool isStatic;
        private int? defaultValueKey;
        private string defaultValueString;
        private bool isDerived;
        private bool isID;
        private readonly List<Attribute> attributes;
        private bool isVirtual;

        public PropertyBuilder()
        {
            this.attributes = new List<Attribute>();
        }

        public Property Build()
        {
            return new Property(
                this.name,
                this.typeReference,
                this.typesRepository,
                this.hasSet,
                this.visibility,
                this.isStatic,
                this.defaultValueKey,
                this.defaultValueString,
                this.isDerived,
                this.isID,
                this.attributes,
                this.isVirtual);
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

        public PropertyBuilder SetTypesRepository(ITypesRepository typesRepository)
        {
            this.typesRepository = typesRepository;
            return this;
        }

        public PropertyBuilder HasSet(bool hasSet)
        {
            this.hasSet = hasSet;
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
    }
}