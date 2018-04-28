using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;

namespace UMLToMVCConverter.CodeTemplates
{
    using System.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.Interfaces;

    public partial class DbContextTextTemplate : IDbContextClassTextTemplate
    {
        private readonly List<Tuple<string, string>> typesNamesAndPlurals;
        private IMvcProject mvcProject;
        private bool onModelCreatingBlock;
        private readonly Dictionary<string, IEnumerable<string>> complexKeys;
        private IEnumerable<EFRelationshipModel> relationships;

        public DbContextTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
            this.typesNamesAndPlurals = new List<Tuple<string, string>>();
            this.onModelCreatingBlock = false;
            this.complexKeys = new Dictionary<string, IEnumerable<string>>();
        }

        public string TransformText(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations, IEnumerable<EFRelationshipModel> relationshipModels)
        {
            this.relationships = relationshipModels;

            foreach (var codeTypeDeclaration in codeTypeDeclarations)
            {
                if (codeTypeDeclaration.HasComplexKey)
                {
                    this.onModelCreatingBlock = true;
                    this.complexKeys.Add(codeTypeDeclaration.Name, codeTypeDeclaration.IDs.Select(x => x.Name));
                }

                var typeName = codeTypeDeclaration.Name;
                string typeNamePlural;
                if (System.Globalization.CultureInfo.CurrentCulture.Name.Substring(0, 1) == "en")
                {
                    var ps = PluralizationService.CreateService(System.Globalization.CultureInfo.CurrentCulture);
                    typeNamePlural = ps.Pluralize(typeName);
                }
                else
                {
                    typeNamePlural = typeName;
                }
                this.typesNamesAndPlurals.Add(new Tuple<string, string>(typeName, typeNamePlural));
            }

            return this.TransformText();
        }
    }
}
