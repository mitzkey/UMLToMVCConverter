namespace UMLToMVCConverter.Domain.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class EFRelationship
    {
        private readonly IEnumerable<string> foreignKeyPropertyNames;

        public EFRelationship(IEnumerable<string> foreignKeyPropertyNames)
        {
            this.foreignKeyPropertyNames = foreignKeyPropertyNames;
        }

        public string PrincipalTypeName { get; set; }

        public string DependentTypeName { get; set; }

        public EFRelationshipMemberMultiplicity PrincipalTypeMultiplicity { get; set; }

        public string ForeignKeysStringEnumeration
        {
            get
            {
                var namesWithQuotes = this.foreignKeyPropertyNames.Select(x => "\"" + x + "\"").ToArray();
                return string.Join(", ", namesWithQuotes);
            }
        }

        public string DeleteBehavior { get; set; }

        public EFRelationshipMemberMultiplicity DependentTypeMultiplicity { get; set; }

        public string HasForeignKeyMethodParametersString =>
            this.DependentTypeMultiplicity.IsMultiple
                ? this.ForeignKeysStringEnumeration
                : $"\"{this.DependentTypeName}\", " + this.ForeignKeysStringEnumeration;
    }
}