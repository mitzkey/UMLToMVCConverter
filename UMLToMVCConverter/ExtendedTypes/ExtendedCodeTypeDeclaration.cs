namespace UMLToMVCConverter.ExtendedTypes
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Reflection;

    public class ExtendedCodeTypeDeclaration : CodeTypeDeclaration
    {
        public List<ExtendedCodeMemberProperty> IDs = new List<ExtendedCodeMemberProperty>();

        public bool HasComplexKey => this.IDs.Count > 1;

        public bool HasKey => this.IDs.Count > 0;

        public bool IsAbstract => this.TypeAttributes.HasFlag(TypeAttributes.Abstract);

        public string BaseClassName => this.BaseTypes.Count > 0
            ? this.BaseTypes[0].BaseType
            : null;

        public ExtendedCodeTypeDeclaration(string name)
            : base(name)
        {
        }

        public ExtendedCodeTypeDeclaration()
        {
        }
    }
}
