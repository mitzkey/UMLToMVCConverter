namespace UMLToMVCConverter.Models
{
    public class Attribute
    {
        public string Name { get; }
        public string Value { get; }

        public Attribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value == null
                ? this.Name
                : $@"{this.Name}(""{this.Value}"")";
        }
    }
}
