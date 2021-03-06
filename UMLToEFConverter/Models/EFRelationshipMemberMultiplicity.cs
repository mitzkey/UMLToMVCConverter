﻿namespace UMLToEFConverter.Models
{
    public class EFRelationshipMemberMultiplicity
    {
        public EFRelationshipMemberMultiplicity(Multiplicity multiplicity)
        {
            this.Multiplicity = multiplicity;
        }

        public string Name =>
            this.Multiplicity == Multiplicity.ZeroOrOne || this.Multiplicity == Multiplicity.ExactlyOne
                ? "One"
                : "Many";

        public bool IsObligatory =>
            this.Multiplicity == Multiplicity.OneOrMore || this.Multiplicity == Multiplicity.ExactlyOne;

        public string IsObligatoryString => this.IsObligatory.ToString().ToLower();

        public Multiplicity Multiplicity { get; set; }

        public bool IsMultiple =>
            this.Multiplicity == Multiplicity.OneOrMore || this.Multiplicity == Multiplicity.ZeroOrMore;
    }
}