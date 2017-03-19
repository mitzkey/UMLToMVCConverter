using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Threading.Tasks;

namespace UMLToMVCConverter.CodeTemplates
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
