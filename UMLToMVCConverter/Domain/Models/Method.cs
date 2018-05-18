namespace UMLToMVCConverter.Domain.Models
{
    using System.CodeDom;
    using System.Collections.Generic;

    public class Method : CodeMemberMethod
    {
        public string Name { get; }

        public ExtendedCodeTypeReference ReturnType { get; }

        public List<ExtendedCodeParameterDeclarationExpression> Parameters { get; }

        public string Visibility { get; }

        public bool IsStatic { get; }


        public Method(
            string name,
            ExtendedCodeTypeReference returnType,
            List<ExtendedCodeParameterDeclarationExpression> parameters,
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