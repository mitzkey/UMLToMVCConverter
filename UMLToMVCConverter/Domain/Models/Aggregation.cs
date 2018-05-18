namespace UMLToMVCConverter.Domain.Models
{
    using System.Xml.Linq;

    public class Aggregation
    {
        public TypeModel PrincipalType { get; set; }

        public TypeModel DependentType { get; set; }

        public AggregationKinds AggregationKind { get; set; }

        public Multiplicity DependentTypeMultiplicity { get; set; }

        public Multiplicity PrincipalTypeMultiplicity { get; set; }

        public XElement DependentTypeAssociationXAttribute { get; set; }

        public XElement PrincipalTypeAssociationXAttribute { get; set; }
    }
}