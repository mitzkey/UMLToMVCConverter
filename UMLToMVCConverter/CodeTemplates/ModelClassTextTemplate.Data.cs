using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.Reflection;

namespace UMLToMVCConverter.CodeTemplates
{
    public partial class ModelClassTextTemplate
    {
        CodeTypeDeclaration _class;
        string contextName;
        string baseClassName;
        bool isAbstract;

        public ModelClassTextTemplate(CodeTypeDeclaration _class, string contextName) {
            this._class = _class;
            isAbstract = _class.TypeAttributes.HasFlag(TypeAttributes.Abstract);
            this.contextName = contextName;
            if (_class.BaseTypes.Count > 0) {
                this.baseClassName = _class.BaseTypes[0].BaseType;
            }
        }

    }
}
