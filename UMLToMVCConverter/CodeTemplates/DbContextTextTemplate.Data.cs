using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;

namespace UMLToMVCConverter.CodeTemplates
{
    using System.CodeDom;
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
        private List<string> customModelBuilderCommands;

        public DbContextTextTemplate(IMvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
            this.typesNamesAndPlurals = new List<Tuple<string, string>>();
            this.onModelCreatingBlock = false;
            this.complexKeys = new Dictionary<string, IEnumerable<string>>();
            this.customModelBuilderCommands = new List<string>();
        }

        public string TransformText(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations, IEnumerable<EFRelationshipModel> relationshipModels, IEnumerable<ExtendedCodeTypeDeclaration> structs)
        {
            this.relationships = relationshipModels;

            var extendedCodeTypeDeclarations = codeTypeDeclarations.ToList();

            foreach (var codeTypeDeclaration in extendedCodeTypeDeclarations)
            {
                if (codeTypeDeclaration.HasComplexKey)
                {
                    this.onModelCreatingBlock = true;
                    this.complexKeys.Add(codeTypeDeclaration.Name, codeTypeDeclaration.PrimaryKeyAttributes.Select(x => x.Name));
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

            foreach (var typeDeclaration in extendedCodeTypeDeclarations)
            {
                foreach (CodeTypeMember typeMember in typeDeclaration.Members)
                {
                    if (typeMember is CodeMemberProperty)
                    {
                        var property = (ExtendedCodeMemberProperty) typeMember;

                        var typeReference = property.ExtendedTypeReference;

                        if (typeReference.ExtType.IsReferencingXmiDeclaredType)
                        {
                            var referencedType =
                                structs.SingleOrDefault(x => typeReference.ExtType.ReferenceTypeXmiID.Equals(x.XmiID));

                            if (referencedType != null)
                            {
                                this.customModelBuilderCommands
                                    .Add($"modelBuilder.Entity<{typeDeclaration.Name}>().OwnsOne(p => p.{property.Name});");
                            }
                        }
                    }
                }
            }

            return this.TransformText();
        }
    }
}
