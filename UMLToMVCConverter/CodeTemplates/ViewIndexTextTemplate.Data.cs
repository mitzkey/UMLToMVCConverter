using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace UMLToMVCConverter.CodeTemplates
{
    public partial class ViewIndexTextTemplate
    {
        string contextName;
        string className;
        List<string> fieldsNames;
        public ViewIndexTextTemplate(CodeTypeDeclaration ctd, string contextName)
        {
            this.contextName = contextName;
            className = ctd.Name;
            fieldsNames = new List<string>();
            foreach (CodeMemberField field in ctd.Members)
            {
                //własciwości oprócz pola "...ID"
                string name = field.Name;
                if (!name.EndsWith("ID"))
                {
                    fieldsNames.Add(name);
                }                
            }
        }
    }
}
