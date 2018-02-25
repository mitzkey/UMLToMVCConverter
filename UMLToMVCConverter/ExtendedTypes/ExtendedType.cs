using System;
using System.Collections.Generic;
using System.Text;

namespace UMLToMVCConverter.ExtendedTypes
{
    public class ExtendedType
    {
        private readonly bool isNamedType;

        private readonly string namedTypeName;

        public static ExtendedType Void => new ExtendedType(typeof(void));

        public Type Type { get; private set; }

        public ExtendedType(Type t, bool isGeneric = false, List<Type> generics = null)
        {
            Type = t;
            this.IsGeneric = isGeneric;
            if (generics != null)
            {
                this.Generics = generics;
            }
            else
            {
                this.Generics = new List<Type>();
            }
        }

        public ExtendedType(string typeName)
        {
            this.isNamedType = true;
            this.namedTypeName = typeName;
        }

        public List<Type> Generics { get; set; }
        public bool IsGeneric { get; set; }
        public string Name { 
            get
            {
                if (this.isNamedType)
                {
                    return this.namedTypeName;
                }

                if (IsGeneric)
                {
                    StringBuilder sb = new StringBuilder();
                    if (Type.Name.Contains("`"))
                    {
                        sb.Append(Type.Name.Substring(0, Type.Name.IndexOf("`")));
                    }
                    else
                    {
                        sb.Append(Type.Name);
                    }
                    sb.Append("<" + string.Join(",", Generics) + ">");

                    return sb.ToString();
                }

                return this.Type.Name;
            }
        }
    }
}
