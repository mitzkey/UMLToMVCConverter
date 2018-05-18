namespace UMLToMVCConverter.Domain.Models
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Reflection;

    public class ExtendedCodeTypeDeclaration : CodeTypeDeclaration
    {
        public List<ExtendedCodeMemberProperty> PrimaryKeyAttributes { get; }

        public bool HasComplexKey => this.PrimaryKeyAttributes.Count > 1;

        public bool HasKey => this.PrimaryKeyAttributes.Count > 0;

        public bool IsAbstract => this.TypeAttributes.HasFlag(TypeAttributes.Abstract);

        public string BaseClassName => this.BaseTypes.Count > 0
            ? this.BaseTypes[0].BaseType
            : null;

        public string XmiID { get; set; }

        public Dictionary<string, ExtendedCodeMemberProperty> ForeignKeys { get; }

        public Dictionary<int, string> Literals { get; set; }

        public ExtendedCodeTypeDeclaration(string name)
            : base(name)
        {
            this.ForeignKeys = new Dictionary<string, ExtendedCodeMemberProperty>();
            this.PrimaryKeyAttributes = new List<ExtendedCodeMemberProperty>();
            this.Literals = new Dictionary<int, string>();
        }
    }
}
