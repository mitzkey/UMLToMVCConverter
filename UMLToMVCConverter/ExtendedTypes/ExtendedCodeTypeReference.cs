using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLToMVCConverter.ExtendedTypes
{
    class ExtendedCodeTypeReference : CodeTypeReference
    {
        public ExtendedType ExtType { get; set; }
        public string ExtTypeName
        {
            get
            {
                return ExtType.Name;
            }
        }
        public bool IsGeneric
        {
            get
            {
                return ExtType.IsGeneric;
            }
        }
        public List<Type> Generics
        {
            get
            {
                return ExtType.Generics;
            }
        }

        public ExtendedCodeTypeReference(Type type, bool isGeneric = false, List<Type> generics = null)
            : base(type)
        {
            ExtType = new ExtendedType(type, isGeneric, generics);
        }

        public ExtendedCodeTypeReference(ExtendedType type)
            : base(type.Type)
        {
            ExtType = type;
        }
    }
}
