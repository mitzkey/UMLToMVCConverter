using System;
using System.CodeDom;
using System.Collections.Generic;

namespace UMLToMVCConverter.ExtendedTypes
{
    public class ExtendedCodeParameterDeclarationExpression : CodeParameterDeclarationExpression
    {
        public ExtendedType ExtType { get; set; }

        public string ExtTypeName => this.ExtType.Name;

        public bool IsGeneric => this.ExtType.IsGeneric;

        public List<Type> Generics => this.ExtType.Generics;

        public ExtendedCodeParameterDeclarationExpression(Type parType, string parName, bool isGeneric = false, List<Type> generics = null)
            : base(parType, parName)
        {
            this.ExtType = new ExtendedType(parType, isGeneric, generics);
        }

        public ExtendedCodeParameterDeclarationExpression(ExtendedType parType, string parName)
            : base(parType.Type, parName)
        {
            this.ExtType = parType;
        }
    }
}
