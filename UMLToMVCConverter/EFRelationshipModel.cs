namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Linq;

    public class EFRelationshipModel
    {
        private readonly IEnumerable<string> foreignKeyPropertyNames;

        public EFRelationshipModel(IEnumerable<string> foreignKeyPropertyNames)
        {
            this.foreignKeyPropertyNames = foreignKeyPropertyNames;
        }

        public string PrincipalTypeName { get; set; }

        public string DependentTypeName { get; set; }

        public RelationshipMultiplicity PrincipalTypeMultiplicity { get; set; }

        public string ForeignKeysStringEnumeration
        {
            get
            {
                var namesWithQuotes = this.foreignKeyPropertyNames.Select(x => "\"" + x + "\"").ToArray();
                return string.Join(", ", namesWithQuotes);
            }
        }

        public string DeleteBehavior { get; set; }

        public RelationshipMultiplicity DependentTypeMultiplicity { get; set; }
    }
}