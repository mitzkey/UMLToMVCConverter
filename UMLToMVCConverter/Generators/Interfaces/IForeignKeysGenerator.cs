namespace UMLToMVCConverter.Generators.Interfaces
{
    using UMLToMVCConverter.Models;

    public interface IForeignKeysGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}