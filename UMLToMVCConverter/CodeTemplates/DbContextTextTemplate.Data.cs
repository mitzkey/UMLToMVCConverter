using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;

namespace UMLToMVCConverter.CodeTemplates
{
    using System.CodeDom;
    using System.Linq;
    using UMLToMVCConverter.Domain.Models;

    public partial class DbContextTextTemplate : IDbContextClassTextTemplate
    {
        private readonly List<Tuple<string, string>> typesNamesAndPlurals;
        private readonly MvcProject mvcProject;
        private bool onModelCreatingBlock;
        private readonly Dictionary<string, IEnumerable<string>> complexKeys;
        private IEnumerable<EFRelationship> relationships;
        private readonly List<string> customModelBuilderCommands;

        public DbContextTextTemplate(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
            this.typesNamesAndPlurals = new List<Tuple<string, string>>();
            this.onModelCreatingBlock = false;
            this.complexKeys = new Dictionary<string, IEnumerable<string>>();
            this.customModelBuilderCommands = new List<string>();
        }

        public string TransformText(
            IEnumerable<ExtendedCodeTypeDeclaration> standaloneEntityTypes,
            IEnumerable<EFRelationship> relationshipModels,
            IEnumerable<ExtendedCodeTypeDeclaration> structs)
        {
            this.relationships = relationshipModels;

            var standaloneEntityTypesList = standaloneEntityTypes.ToList();

            foreach (var type in standaloneEntityTypesList)
            {
                if (type.HasComplexKey)
                {
                    this.onModelCreatingBlock = true;
                    this.complexKeys.Add(type.Name, type.PrimaryKeyAttributes.Select(x => x.Name));
                }

                var typeName = type.Name;
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

            foreach (var typeDeclaration in standaloneEntityTypesList)
            {
                foreach (CodeTypeMember typeMember in typeDeclaration.Members)
                {
                    if (typeMember is CodeMemberProperty)
                    {
                        var property = (Property) typeMember;

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

                        if (property.HasDefaultValueKey)
                        {
                            this.customModelBuilderCommands
                                .Add($"modelBuilder.Entity<{typeDeclaration.Name}>().Property(b => b.{property.Name}ID).HasDefaultValueSql(\"{property.DefaultValueKey}\");");
                        }
                    }
                }
            }

            return this.TransformText();
        }
    }
}
