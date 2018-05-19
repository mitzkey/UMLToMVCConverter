namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class Property
    {
        private string defaultValueString;

        private IReadOnlyDictionary<Type, string> defaultValueFormats;

        public Property(
            string name,
            TypeReference typeReference,
            ITypesRepository typesRepository,
            bool hasSet,
            string visibility,
            bool isStatic,
            int? defaultValueKey = null,
            string defaultValueString = null,
            bool isDerived = false,
            bool isID = false)
        {
            this.DefaultValueString = defaultValueString ?? string.Empty;
            this.Name = name;
            this.TypeReference = typeReference;
            this.HasSet = hasSet;
            this.Visibility = visibility;
            this.IsStatic = isStatic;
            this.DefaultValueKey = defaultValueKey;
            this.IsDerived = isDerived;
            this.IsID = isID;
            if (typeReference.IsReferencingXmiDeclaredType)
            {
                this.ReferencingType = typesRepository.GetTypeByXmiId(typeReference.ReferenceTypeXmiID);
            }
        }

        public string Name { get; }

        public TypeReference TypeReference { get; }

        public TypeModel ReferencingType { get; set; }

        public bool HasDefaultValueString => !string.IsNullOrWhiteSpace(this.defaultValueString);

        public string DefaultValueString
        {
            get
            {
                var type = this.TypeReference.Type;
                var result = this.defaultValueFormats[type];
                if (result == null)
                {
                    throw new NotSupportedException("Default value for type: " + type + " not supported.");
                }
                return result;
            }

            set
            {
                this.defaultValueString = value;

                this.defaultValueFormats = new Dictionary<Type, string> {
                    { typeof(string), "\"" + this.defaultValueString + "\"" },
                    { typeof(int), this.defaultValueString },
                    { typeof(double), this.defaultValueString + "d" },
                    { typeof(bool), this.defaultValueString.ToLower() }
                };
            }
        }

        public bool IsDerived { get; set; }

        public bool IsID { get; set; }

        public bool IsVirtual { get; set; }

        public bool HasDefaultValueKey => this.DefaultValueKey != null;

        public int? DefaultValueKey { get; set; }

        public bool IsReferencingType => this.ReferencingType != null;

        public bool IsReferencingEnumType => this.IsReferencingType && this.ReferencingType.IsEnum;

        public bool HasSet { get; set; }

        public string Visibility { get; }

        public bool IsStatic { get; }
    }
}
