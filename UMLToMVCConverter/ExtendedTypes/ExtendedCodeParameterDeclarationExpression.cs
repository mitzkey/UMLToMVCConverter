using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLToMVCConverter.ExtendedTypes
{
    class ExtendedCodeParameterDeclarationExpression : CodeParameterDeclarationExpression
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

        public ExtendedCodeParameterDeclarationExpression(Type parType, string parName, bool isGeneric = false, List<Type> generics = null)
            : base(parType, parName)
        {
            ExtType = new ExtendedType(parType, isGeneric, generics);
        }

        public ExtendedCodeParameterDeclarationExpression(ExtendedType parType, string parName)
            : base(parType.Type, parName)
        {
            ExtType = parType;
        }
    }
}
