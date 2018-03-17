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

        public bool IsBasic { get; }
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

        public ExtendedType(Type t, bool isBasic, bool isGeneric = false, IEnumerable<ExtendedType> generics = null, bool isCollection = false)
        {
            this.Type = t;
            this.IsGeneric = isGeneric;
            this.IsCollection = isCollection;
            this.IsBasic = isBasic;
            this.Generics = generics?.ToList() ?? new List<ExtendedType>();
        }

        public ExtendedType(string typeName, bool isBasic)
        {
            this.IsNamedType = true;
            this.IsBasic = isBasic;
            this.namedTypeName = typeName;
        }
    }
}
