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
        string mvcProjectName;
        string baseClassName;
        bool isAbstract;

        public ModelClassTextTemplate(CodeTypeDeclaration _class, string mvcProjectName) {
            this._class = _class;
            isAbstract = _class.TypeAttributes.HasFlag(TypeAttributes.Abstract);
            this.mvcProjectName = mvcProjectName;
            if (_class.BaseTypes.Count > 0) {
                this.baseClassName = _class.BaseTypes[0].BaseType;
            }
        }

    }
}
