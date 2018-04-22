namespace UMLToMVCConverter.ExtendedTypes
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Reflection;

    public class ExtendedCodeTypeDeclaration : CodeTypeDeclaration
    {
        public List<ExtendedCodeMemberProperty> IDs { get; }

        public bool HasComplexKey => this.IDs.Count > 1;

        public bool HasKey => this.IDs.Count > 0;

        public bool IsAbstract => this.TypeAttributes.HasFlag(TypeAttributes.Abstract);

        public string BaseClassName => this.BaseTypes.Count > 0
            ? this.BaseTypes[0].BaseType
            : null;

        public string XmiID { get; set; }

        public Dictionary<string, ExtendedCodeMemberProperty> ForeignKeys { get; }

        public ExtendedCodeTypeDeclaration(string name)
            : base(name)
        {
            this.ForeignKeys = new Dictionary<string, ExtendedCodeMemberProperty>();
            this.IDs = new List<ExtendedCodeMemberProperty>();
        }
    }
}
