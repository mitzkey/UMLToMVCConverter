namespace UMLToMVCConverter.Models
{
    using System;
    using System.Collections.Generic;
    using UMLToMVCConverter.Models.Builders;

    public class Property
    {
        private string defaultValueString;

        private IReadOnlyDictionary<Type, string> defaultValueFormats;

        public Property(
            string name,
            TypeReference typeReference,
            bool isReadOnly,
            string visibility,
            bool isStatic,
            int? defaultValueKey,
            string defaultValueString,
            bool isDerived,
            bool isID,
            List<Attribute> attributes,
            bool isVirtual,
            bool isReferencingEnumType)
        {
            this.DefaultValueString = defaultValueString ?? string.Empty;
            this.Name = name;
            this.TypeReference = typeReference;
            this.IsReadOnly = isReadOnly;
            this.Visibility = visibility;
            this.IsStatic = isStatic;
            this.DefaultValueKey = defaultValueKey;
            this.IsDerived = isDerived;
            this.IsID = isID;
            this.IsVirtual = isVirtual;
            this.IsReferencingEnumType = isReferencingEnumType;
            this.Attributes = attributes ?? new List<Attribute>();
        }

        public string Name { get; set;  }

        public TypeReference TypeReference { get; }

        public TypeModel ReferencingType { get; set; }

        public bool HasDefaultValueString => !string.IsNullOrWhiteSpace(this.defaultValueString);

        public string DefaultValueString
        {
            get
            {
                var type = this.TypeReference.Type == typeof(Nullable)
                    ? this.TypeReference.Generic.Type
                    : this.TypeReference.Type;
                var result = this.defaultValueFormats[type];
                if (result == null)
                {
                    throw new NotSupportedException($"Default value for type: { type } not supported.");
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
                    { typeof(bool), this.defaultValueString.ToLower() },
                    { typeof(System.Int64), this.defaultValueString }
                };
            }
        }

        public bool IsDerived { get; set; }

        public bool IsID { get; set; }

        public List<Attribute> Attributes { get; }

        public bool IsVirtual { get; set; }

        public bool HasDefaultValueKey => this.DefaultValueKey != null;

        public int? DefaultValueKey { get; set; }

        public bool IsReferencingEnumType { get;}

        public bool IsReadOnly { get; set; }

        public string Visibility { get; }

        public bool IsStatic { get; }

        public static PropertyBuilder Builder()
        {
            return new PropertyBuilder();
        }
    }
}
