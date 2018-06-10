using System.CodeDom;

namespace UMLToEFConverter.CodeTemplates
{
    public partial class ControllerTextTemplate
    {
        string contextName;
        string className;
        public ControllerTextTemplate(CodeTypeDeclaration ctd, string contextName)
        {
            this.contextName = contextName;
            this.className = ctd.Name;
        }
    }
}
