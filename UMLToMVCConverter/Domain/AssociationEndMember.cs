namespace UMLToMVCConverter.Domain
{
    public class AssociationEndMember
    {
        public Multiplicity Multiplicity { get; }

        public string Name { get; }

        public string XmiId { get; }

        public AssociationEndMember(string xmiId, string name, Multiplicity multiplicity)
        {
            this.Multiplicity = multiplicity;
            this.Name = name;
            this.XmiId = xmiId;
        }
    }
}