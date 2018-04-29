namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public class Aggregation
    {
        public ExtendedCodeTypeDeclaration PrincipalType { get; set; }

        public ExtendedCodeTypeDeclaration DependentType { get; set; }

        public AggregationKinds AggregationKind { get; set; }

        public Multiplicity DependentTypeMultiplicity { get; set; }

        public Multiplicity PrincipalTypeMultiplicity { get; set; }

        public XElement DependentTypeAssociationXAttribute { get; set; }

        public XElement PrincipalTypeAssociationXAttribute { get; set; }
    }
}