namespace UMLToEFConverter.Models
{
    public class AssociationEndMember
    {
        public Multiplicity Multiplicity { get; set;  }

        public AssociationKind AssociationKind { get; }

        public string Name { get; }

        public string XmiId { get; }

        public TypeModel Type { get; }

        public bool Navigable { get; }

        public AssociationEndMember(string xmiId, string name, Multiplicity multiplicity, AssociationKind associationKind, TypeModel type, bool navigable)
        {
            this.Multiplicity = multiplicity;
            this.AssociationKind = associationKind;
            this.Type = type;
            this.Navigable = navigable;
            this.Name = name;
            this.XmiId = xmiId;
        }
    }
}