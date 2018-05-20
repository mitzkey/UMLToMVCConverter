namespace UMLToMVCConverter.Domain
{
    using System.Xml.Linq;

    public interface IAssociationFactory
    {
        Association Create(XElement xAssociation);
    }
}