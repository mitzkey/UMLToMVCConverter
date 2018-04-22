namespace UMLToMVCConverter.CodeTemplates
{
    public interface IRelationship
    {
        string SourceEntityName { get; set; }

        string TargetEntityName { get; set; }

        RelationshipMultiplicity Multiplicity { get; set; }

        string ForeignKeysStringEnumeration { get; }

        string DeleteBehavior { get; set; }
    }
}