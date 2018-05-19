namespace UMLToMVCConverter.Domain.Models
{
    using System.Collections.Generic;

    public class Method
    {
        public string Name { get; }

        public List<MethodParameter> Parameters { get; }

        public string Visibility { get; }

        public bool IsStatic { get; }

        public TypeReference ReturnType { get; }

        public Method(
            string name,
            TypeReference returnType,
            List<MethodParameter> parameters,
            string visibility,
            bool isStatic)
        {
            this.Name = name;
            this.ReturnType = returnType;
            this.Parameters = parameters;
            this.Visibility = visibility;
            this.IsStatic = isStatic;
        }
    }
}