namespace UMLToMVCConverter.Domain.Generators.Interfaces
{
    using UMLToMVCConverter.Domain.Models;

    public interface IForeignKeysGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}