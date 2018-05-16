namespace UMLToMVCConverter.Domain
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;

    public class ExtendedCodeMemberMethod : CodeMemberMethod
    {
        public ExtendedCodeMemberMethod(
            string visibility,
            CodeTypeReference returnType,
            string name,
            IEnumerable<CodeParameterDeclarationExpression> parameters)
        {
            this.Visibility = visibility;
            base.ReturnType = returnType;
            base.Name = name;
            base.Parameters.AddRange(parameters.ToArray());
        }

        public string Visibility { get; set; }

        public bool IsStatic { get; set; }
    }
}
