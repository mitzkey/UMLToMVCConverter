namespace UMLToEFConverter.Generators.Interfaces
{
    using UMLToEFConverter.Models;

    public interface INavigationalPropertiesGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}