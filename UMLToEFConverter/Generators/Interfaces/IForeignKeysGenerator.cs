namespace UMLToEFConverter.Generators.Interfaces
{
    using UMLToEFConverter.Models;

    public interface IForeignKeysGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}