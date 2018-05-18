namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;

    public class ExtendedCodeTypeReference : CodeTypeReference
    {
        public ExtendedType ExtType { get; set; }

        public string ExtTypeName => this.ExtType.Name;

        public bool IsGeneric => this.ExtType.IsGeneric;

        public List<ExtendedType> Generics => this.ExtType.Generics;

        public bool IsNamedType => this.ExtType.IsNamedType;

        private ExtendedCodeTypeReference(Type type, bool isBaseType, bool isGeneric = false, IEnumerable<ExtendedType> generics = null)
            : base(type)
        {
            this.ExtType = new ExtendedType(type, isBaseType, isGeneric, generics);
        }

        public ExtendedCodeTypeReference(ExtendedType type)
            : base(type.Type)
        {
            this.ExtType = type;
        }

        private ExtendedCodeTypeReference(string typeName, bool isBaseType, string referenceTypeXmiID)
            : base(typeName)
        {
            this.ExtType = new ExtendedType(typeName, isBaseType, referenceTypeXmiID);
        }

        public static ExtendedCodeTypeReference CreateForType(ExtendedType type)
        {
            if (type.Type == null)
            {
                return new ExtendedCodeTypeReference(type.Name, type.IsBaseType, type.ReferenceTypeXmiID);
            }
            else
            {
                return new ExtendedCodeTypeReference(type);
            }
        }
    }
}
