namespace UMLToMVCConverter.Domain.Models
{
    public class MethodParameter
    {
        public TypeReference TypeReference { get; set; }

        public string Name { get; }

        public string ExtTypeName => this.TypeReference.Name;

        public MethodParameter(TypeReference parameterType, string name)
        {
            this.TypeReference = parameterType;
            this.Name = name;
        }
    }
}
