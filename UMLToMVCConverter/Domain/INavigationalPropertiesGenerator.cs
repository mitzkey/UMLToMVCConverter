namespace UMLToMVCConverter.Domain
{
    public interface INavigationalPropertiesGenerator
    {
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}