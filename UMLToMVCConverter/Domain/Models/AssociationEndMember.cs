namespace UMLToMVCConverter.Domain.Models
{
    public class AssociationEndMember
    {
        public Multiplicity Multiplicity { get; }

        public AggregationKind AggregationKind { get; }

        public string Name { get; }

        public string XmiId { get; }

        public TypeModel Type { get; }

        public bool Navigable { get; }

        public AssociationEndMember(string xmiId, string name, Multiplicity multiplicity, AggregationKind aggregationKind, TypeModel type, bool navigable)
        {
            this.Multiplicity = multiplicity;
            this.AggregationKind = aggregationKind;
            this.Type = type;
            this.Navigable = navigable;
            this.Name = name;
            this.XmiId = xmiId;
        }
    }
}