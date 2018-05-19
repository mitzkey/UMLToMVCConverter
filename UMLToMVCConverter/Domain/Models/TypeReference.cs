namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TypeReference
    {
        private readonly string namedTypeName;

        public Type Type { get; }

        public bool IsNamedType { get; }

        public bool IsCollection { get; }

        public bool IsBaseType { get; }

        public string ReferenceTypeXmiID { get; }

        public bool IsReferencingXmiDeclaredType => !string.IsNullOrWhiteSpace(this.ReferenceTypeXmiID);

        public List<TypeReference> Generics { get; set; }

        public bool IsGeneric { get; set; }

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
                    sb.Append("<" + string.Join(",", this.Generics.Select(t => t.Name)) + ">");

                    return sb.ToString();
                }

                return this.Type.FullName;
            }
        }

        public bool IsNullable { get; private set; }

        public TypeReference(
            Type type, 
            bool isBaseType, 
            bool isGeneric = false, 
            IEnumerable<TypeReference> generics = null, 
            bool isCollection = false)
        {
            this.Type = type;
            this.IsGeneric = isGeneric;
            this.IsCollection = isCollection;
            this.IsBaseType = isBaseType;
            this.Generics = generics?.ToList() ?? new List<TypeReference>();
        }

        public TypeReference(string typeName, bool isBaseType)
        {
            this.IsNamedType = true;
            this.IsBaseType = isBaseType;
            this.namedTypeName = typeName;
        }

        public TypeReference(
            Type type,
            string name,
            bool isBaseType,
            bool isGeneric,
            IEnumerable<TypeReference> generics,
            bool isCollection,
            string referenceTypeXmiID,
            bool isNamedType)
        {
            this.Type = type;
            this.IsBaseType = isBaseType;
            this.IsGeneric = isGeneric;
            this.IsCollection = isCollection;
            this.ReferenceTypeXmiID = referenceTypeXmiID;
            this.IsNamedType = isNamedType;
            this.Generics = generics.ToList();
            this.namedTypeName = name;
        }

        public static TypeReferenceBuilder Builder()
        {
            return new TypeReferenceBuilder();
        }
    }
}
