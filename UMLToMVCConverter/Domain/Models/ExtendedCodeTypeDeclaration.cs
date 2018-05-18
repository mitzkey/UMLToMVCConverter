namespace UMLToMVCConverter.Domain.Models
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Reflection;

    public class ExtendedCodeTypeDeclaration : CodeTypeDeclaration
    {
        public List<Property> PrimaryKeyAttributes { get; }

        public bool HasComplexKey => this.PrimaryKeyAttributes.Count > 1;

        public bool HasKey => this.PrimaryKeyAttributes.Count > 0;

        public bool IsAbstract => this.TypeAttributes.HasFlag(TypeAttributes.Abstract);

        public string BaseClassName => this.BaseTypes.Count > 0
            ? this.BaseTypes[0].BaseType
            : null;

        public string XmiID { get; set; }

        public Dictionary<string, Property> ForeignKeys { get; }

        public Dictionary<int, string> Literals { get; set; }

        public List<Method> Methods { get; }

        public ExtendedCodeTypeDeclaration(string name)
            : base(name)
        {
            this.Methods = new List<Method>();
            this.ForeignKeys = new Dictionary<string, Property>();
            this.PrimaryKeyAttributes = new List<Property>();
            this.Literals = new Dictionary<int, string>();
        }
    }
}
