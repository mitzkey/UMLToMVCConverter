namespace UMLToMVCConverter.Domain
{
    using UMLToMVCConverter.Domain.Models;

    public class AssociationEndMember
    {
        public Multiplicity Multiplicity { get; }

        public AggregationKind AggregationKind { get; }

        public string Name { get; }

        public string XmiId { get; }
        public TypeModel Type { get; }

        public AssociationEndMember(string xmiId, string name, Multiplicity multiplicity, AggregationKind aggregationKind, TypeModel type)
        {
            this.Multiplicity = multiplicity;
            this.AggregationKind = aggregationKind;
            this.Type = type;
            this.Name = name;
            this.XmiId = xmiId;
        }
    }
}