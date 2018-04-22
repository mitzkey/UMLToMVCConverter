namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using System.Linq;

    public class Composition : IRelationship
    {
        private readonly IEnumerable<string> foreignKeyPropertyNames;

        public Composition(IEnumerable<string> foreignKeyPropertyNames)
        {
            this.foreignKeyPropertyNames = foreignKeyPropertyNames;
        }

        public string SourceEntityName { get; set; }

        public string TargetEntityName { get; set; }

        public RelationshipMultiplicity Multiplicity { get; set; }

        public string ForeignKeysStringEnumeration
        {
            get
            {
                var namesWithQuotes = this.foreignKeyPropertyNames.Select(x => "\"" + x + "\"").ToArray();
                return string.Join(", ", namesWithQuotes);
            }
        }

        public string DeleteBehavior { get; set; }
    }
}