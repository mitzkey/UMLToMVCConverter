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
        List<CodeTypeDeclaration> classes;
        List<Tuple<string, string>> classesNamesAndPlurals;
        string contextName;

        public DbContextTextTemplate(List<CodeTypeDeclaration> classes, string contextName)
        {
            this.classes = classes;
            this.contextName = contextName;
            classesNamesAndPlurals = new List<Tuple<string, string>>();
            foreach (CodeTypeDeclaration ctd in classes)
            {
                string className = ctd.Name;
                string classNamePlural;
                if (System.Globalization.CultureInfo.CurrentCulture.Name.Substring(0, 1) == "en")
                {
                    PluralizationService ps = PluralizationService.CreateService(System.Globalization.CultureInfo.CurrentCulture);
                    classNamePlural = ps.Pluralize(className);
                }
                else 
                {
                    classNamePlural = className + "Set";
                }
                classesNamesAndPlurals.Add(new Tuple<string,string>(className, classNamePlural));
            }
        }
    }
}
