namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class MethodParameter
    {
        public TypeReference TypeReference { get; set; }

        public string Name { get; }

        public string ExtTypeName => this.TypeReference.Name;

        public bool IsGeneric => this.TypeReference.IsGeneric;

        public List<TypeReference> Generics => this.TypeReference.Generics;

        public MethodParameter(
            Type type,
            string name,
            bool isBasic,
            bool isGeneric = false,
            IEnumerable<TypeReference> generics = null)
        {
            this.Name = name;
            this.TypeReference = new TypeReference(type, isBasic, isGeneric, generics);
        }

        public MethodParameter(TypeReference parameterType, string name)
        {
            this.TypeReference = parameterType;
            this.Name = name;
        }
    }
}
