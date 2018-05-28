namespace UMLToMVCConverter.Models
{
    public class Attribute
    {
        public string Name { get; }

        public string Value { get; }
        public bool ValueIsString { get; }

        public Attribute(string name, string value, bool valueIsString = true)
        {
            this.Name = name;
            this.Value = value;
            this.ValueIsString = valueIsString;
        }

        public override string ToString()
        {
            if (!this.ValueIsString)
            {
                return this.Value == null
                    ? this.Name
                    : $@"{this.Name}({this.Value})";
            }

            return this.Value == null
                ? this.Name
                : $@"{this.Name}(""{this.Value}"")";
        }
    }
}
