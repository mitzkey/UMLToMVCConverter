namespace UMLToMVCConverter.Domain.Factories.Interfaces
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Models;

    public interface IAssociationFactory
    {
        Association Create(XElement xAssociation);
    }
}