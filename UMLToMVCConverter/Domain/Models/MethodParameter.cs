namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class MethodParameter
    {
        public ExtendedType ExtType { get; set; }

        public string Name { get; }

        public string ExtTypeName => this.ExtType.Name;

        public bool IsGeneric => this.ExtType.IsGeneric;

        public List<ExtendedType> Generics => this.ExtType.Generics;

        public MethodParameter(
            Type type,
            string name,
            bool isBasic,
            bool isGeneric = false,
            IEnumerable<ExtendedType> generics = null)
        {
            this.Name = name;
            this.ExtType = new ExtendedType(type, isBasic, isGeneric, generics);
        }

        public MethodParameter(ExtendedType parType, string name)
        {
            this.ExtType = parType;
            this.Name = name;
        }
    }
}
