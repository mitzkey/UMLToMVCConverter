using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace UMLToMVCConverter.CodeTemplates
{
    public partial class ModelClassTextTemplate
    {
        CodeTypeDeclaration _class;
        string contextName;
        string baseClassName;

        public ModelClassTextTemplate(CodeTypeDeclaration _class, string contextName) {
            this._class = _class;
            this.contextName = contextName;
            if (_class.BaseTypes.Count > 0) {
                this.baseClassName = _class.BaseTypes[0].BaseType;
            }
        }

    }
}
