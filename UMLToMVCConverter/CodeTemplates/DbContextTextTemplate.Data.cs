using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.Data.Entity.Design.PluralizationServices;

namespace UMLToMVCConverter.CodeTemplates
{
    public partial class DbContextTextTemplate
    {
        private readonly List<Tuple<string, string>> typesNamesAndPlurals;
        private readonly string contextName;
        private readonly string mvcProjectName;

        public DbContextTextTemplate(IEnumerable<CodeTypeDeclaration> codeTypeDeclarations, string contextName, string mvcProjectName)
        {
            this.contextName = contextName;
            this.mvcProjectName = mvcProjectName;
            this.typesNamesAndPlurals = new List<Tuple<string, string>>();
            foreach (var ctd in codeTypeDeclarations)
            {
                string typeName = ctd.Name;
                string typeNamePlural;
                if (System.Globalization.CultureInfo.CurrentCulture.Name.Substring(0, 1) == "en")
                {
                    var ps = PluralizationService.CreateService(System.Globalization.CultureInfo.CurrentCulture);
                    typeNamePlural = ps.Pluralize(typeName);
                }
                else 
                {
                    typeNamePlural = typeName + "Set";
                }
                this.typesNamesAndPlurals.Add(new Tuple<string,string>(typeName, typeNamePlural));
            }
        }
    }
}
