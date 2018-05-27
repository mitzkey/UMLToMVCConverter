namespace UMLToMVCConverter.Domain.Models
{
    public class EFRelationship
    {
        public string SourceTypeName { get; set; }

        public string SourceNavigationalPropertyName { get; set; }

        public string TargetNavigationalPropertyName { get; set; }

        public EFRelationshipMemberMultiplicity SourceMemberMultiplicity { get; set; }

        public EFRelationshipMemberMultiplicity TargetMemberMultiplicity { get; set; }

        public string DeleteBehavior { get; set; }

        public bool IsSourceMemberNavigable { get; set; }

        public bool IsTargetMemberNavigable { get; set; }

        public string SourceNavigationalPropertyExpressionString => this.IsSourceMemberNavigable
            ? $"t => t.{ this.SourceNavigationalPropertyName }"
            : string.Empty;

        public string TargetNavigationalPropertyExpressionString => this.IsTargetMemberNavigable
            ? $"t => t.{ this.TargetNavigationalPropertyName }"
            : string.Empty;
    }
}