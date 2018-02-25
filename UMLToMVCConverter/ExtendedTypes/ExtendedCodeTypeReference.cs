using System;
using System.CodeDom;
using System.Collections.Generic;

namespace UMLToMVCConverter.ExtendedTypes
{
    public class ExtendedCodeTypeReference : CodeTypeReference
    {
        public ExtendedType ExtType { get; set; }

        public string ExtTypeName => this.ExtType.Name;

        public bool IsGeneric => this.ExtType.IsGeneric;

        public List<Type> Generics => this.ExtType.Generics;

        public bool IsNametType => this.ExtType.IsNamedType;

        private ExtendedCodeTypeReference(Type type, bool isGeneric = false, List<Type> generics = null)
            : base(type)
        {
            this.ExtType = new ExtendedType(type, isGeneric, generics);
        }

        private ExtendedCodeTypeReference(ExtendedType type)
            : base(type.Type)
        {
            this.ExtType = type;
        }

        private ExtendedCodeTypeReference(string typeName)
            : base(typeName)
        {
            this.ExtType = new ExtendedType(typeName);
        }

        public static ExtendedCodeTypeReference CreateForType(ExtendedType type)
        {
            if (type.Type == null)
            {
                return new ExtendedCodeTypeReference(type.Name);
            }
            else
            {
                return new ExtendedCodeTypeReference(type);
            }
        }
    }
}
