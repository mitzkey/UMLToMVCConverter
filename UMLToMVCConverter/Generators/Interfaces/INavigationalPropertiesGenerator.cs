namespace UMLToMVCConverter.Generators.Interfaces
{
    using UMLToMVCConverter.Models;

    public interface INavigationalPropertiesGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}