using System;
using System.Collections.Generic;
using System.CodeDom;
using System.Data.Entity.Design.PluralizationServices;

namespace UMLToMVCConverter.CodeTemplates
{
    public partial class DbContextTextTemplate : IDbContextClassTextTemplate
    {
        private readonly List<Tuple<string, string>> typesNamesAndPlurals;
        private IMvcProject mvcProject;

        public DbContextTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
            this.typesNamesAndPlurals = new List<Tuple<string, string>>();

        }

        public string TransformText(List<CodeTypeDeclaration> codeTypeDeclarations)
        {
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
                this.typesNamesAndPlurals.Add(new Tuple<string, string>(typeName, typeNamePlural));
            }

            return this.TransformText();
        }
    }
}
