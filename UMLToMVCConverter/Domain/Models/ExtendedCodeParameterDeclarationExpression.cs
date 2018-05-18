namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;

    public class ExtendedCodeParameterDeclarationExpression : CodeParameterDeclarationExpression
    {
        public ExtendedType ExtType { get; set; }

        public string ExtTypeName => this.ExtType.Name;

        public bool IsGeneric => this.ExtType.IsGeneric;

        public List<ExtendedType> Generics => this.ExtType.Generics;

        public ExtendedCodeParameterDeclarationExpression(Type parType, string parName, bool isBasic, bool isGeneric = false, IEnumerable<ExtendedType> generics = null)
            : base(parType, parName)
        {
            this.ExtType = new ExtendedType(parType, isBasic, isGeneric, generics);
        }

        public ExtendedCodeParameterDeclarationExpression(ExtendedType parType, string parName)
            : base(parType.Type, parName)
        {
            this.ExtType = parType;
        }
    }
}
