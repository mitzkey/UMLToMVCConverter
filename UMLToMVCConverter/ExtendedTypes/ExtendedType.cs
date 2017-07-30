using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLToMVCConverter.ExtendedTypes
{
    class ExtendedType
    {
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
        public List<Type> Generics { get; set; }
        public bool IsGeneric { get; set; }
        public string Name { 
            get
            {
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
                else
                {
                    return Type.Name;
                }
            }
        }
    }
}
