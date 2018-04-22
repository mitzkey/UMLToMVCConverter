namespace UMLToMVCConverter.CodeTemplates
{
    public class RelationshipMultiplicity
    {
        public string Name { get; set; }

        public bool IsObligatory { get; set; }

        public string IsObligatoryString => this.IsObligatory.ToString().ToLower();
    }
}