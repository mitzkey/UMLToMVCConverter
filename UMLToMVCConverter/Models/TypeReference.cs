namespace UMLToMVCConverter.Models
{
    using System;
    using System.Text;
    using UMLToMVCConverter.Models.Builders;

    public class TypeReference
    {
        private readonly string namedTypeName;

        public Type Type { get; }

        public bool IsNamedType { get; }

        public bool IsCollection { get; }

        public bool IsBaseType { get; }

        public string ReferenceTypeXmiID { get; }

        public bool IsReferencingXmiDeclaredType => !string.IsNullOrWhiteSpace(this.ReferenceTypeXmiID);

        public TypeReference Generic { get; set; }

        public bool IsGeneric { get; set; }

        public bool IsComplexType { get; }

        public bool IsPrimitive { get; }

        public bool IsNullable { get; }

        public string Name
        {
            get
            {
                if (this.IsNamedType)
                {
                    return this.namedTypeName;
                }

                if (this.IsGeneric)
                {
                    var sb = new StringBuilder();
                    sb.Append(
                        this.Type.Name.Contains("`")
                            ? this.Type.Name.Substring(0, this.Type.Name.IndexOf("`", StringComparison.Ordinal))
                            : this.Type.Name);
                    sb.Append($"<{this.Generic.Name}>");

                    return sb.ToString();
                }

                return this.Type.FullName;
            }
        }

        public TypeReference(
            Type type,
            string name,
            bool isBaseType,
            bool isGeneric,
            TypeReference generic,
            bool isCollection,
            string referenceTypeXmiID,
            bool isNamedType,
            bool isNullable,
            bool isComplexType,
            bool isPrimitive)
        {
            this.Type = type;
            this.IsBaseType = isBaseType;
            this.IsGeneric = isGeneric;
            this.IsCollection = isCollection;
            this.ReferenceTypeXmiID = referenceTypeXmiID;
            this.IsNamedType = isNamedType;
            this.IsNullable = isNullable;
            this.Generic = generic;
            this.namedTypeName = name;
            this.IsComplexType = isComplexType;
            this.IsPrimitive = isPrimitive;
        }

        public static TypeReferenceBuilder Builder()
        {
            return new TypeReferenceBuilder();
        }
    }
}
