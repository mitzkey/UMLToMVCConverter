namespace UMLToMVCConverter.ExtendedTypes
{
    using System.CodeDom;
    using System.Collections.Generic;

    public class ExtendedCodeTypeDeclaration : CodeTypeDeclaration
    {
        public List<ExtendedCodeMemberProperty> IDs = new List<ExtendedCodeMemberProperty>();

        public bool HasComplexKey => this.IDs.Count > 1;

        public bool HasKey => this.IDs.Count > 0;

        public ExtendedCodeTypeDeclaration(string name)
            : base(name)
        {
        }

        public ExtendedCodeTypeDeclaration()
        {
        }
    }
}
