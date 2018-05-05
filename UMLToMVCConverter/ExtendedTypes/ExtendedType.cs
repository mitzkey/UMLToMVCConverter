﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UMLToMVCConverter.ExtendedTypes
{
    using System.Linq;

    public class ExtendedType
    {
        private readonly string namedTypeName;

        public static ExtendedType Void => new ExtendedType(typeof(void), true);

        public Type Type { get; }

        public bool IsNamedType { get; }

        public bool IsCollection { get; }

        public bool IsBaseType { get; }

        public string ReferenceTypeXmiID { get; }

        public bool IsReferencingXmiDeclaredType => !string.IsNullOrWhiteSpace(this.ReferenceTypeXmiID);

        public List<ExtendedType> Generics { get; set; }

        public bool IsGeneric { get; set; }

        public string Name
        {
            get
            {
                if (this.IsNamedType)
                {
                    return this.namedTypeName;
                }

                if (IsGeneric)
                {
                    var sb = new StringBuilder();
                    sb.Append(
                        this.Type.Name.Contains("`")
                            ? this.Type.Name.Substring(0, this.Type.Name.IndexOf("`", StringComparison.Ordinal))
                            : this.Type.Name);
                    sb.Append("<" + string.Join(",", this.Generics.Select(t => t.Name)) + ">");

                    return sb.ToString();
                }

                return this.Type.Name;
            }
        }

        public ExtendedType(Type t, bool isBaseType, bool isGeneric = false, IEnumerable<ExtendedType> generics = null, bool isCollection = false)
        {
            this.Type = t;
            this.IsGeneric = isGeneric;
            this.IsCollection = isCollection;
            this.IsBaseType = isBaseType;
            this.Generics = generics?.ToList() ?? new List<ExtendedType>();
        }

        public ExtendedType(string typeName, bool isBaseType, string referenceTypeXmiID)
        {
            this.IsNamedType = true;
            this.IsBaseType = isBaseType;
            this.ReferenceTypeXmiID = referenceTypeXmiID;
            this.namedTypeName = typeName;
        }

        public ExtendedType(string typeName, bool isBaseType)
        {
            this.IsNamedType = true;
            this.IsBaseType = isBaseType;
            this.namedTypeName = typeName;
        }
    }
}
