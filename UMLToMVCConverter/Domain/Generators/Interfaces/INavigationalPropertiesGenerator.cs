namespace UMLToMVCConverter.Domain.Generators.Interfaces
{
    using UMLToMVCConverter.Domain.Models;

    public interface INavigationalPropertiesGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}