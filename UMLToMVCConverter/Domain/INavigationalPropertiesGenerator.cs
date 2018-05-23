namespace UMLToMVCConverter.Domain
{
    public interface INavigationalPropertiesGenerator
    {
        void Generate(AssociationEndMember dependentMember, AssociationEndMember principalMember);
    }
}