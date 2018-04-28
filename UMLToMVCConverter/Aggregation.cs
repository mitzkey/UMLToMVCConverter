namespace UMLToMVCConverter
{
    using UMLToMVCConverter.ExtendedTypes;

    public class Aggregation
    {
        public ExtendedCodeTypeDeclaration CompositeType { get; set; }

        public ExtendedCodeTypeDeclaration ComposedType { get; set; }

        public AggregationKinds AggregationKind { get; set; }
    }
}