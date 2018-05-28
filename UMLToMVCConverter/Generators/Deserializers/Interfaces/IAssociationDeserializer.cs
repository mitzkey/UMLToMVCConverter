namespace UMLToMVCConverter.Generators.Deserializers.Interfaces
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IAssociationDeserializer
    {
        Association Create(XElement xAssociation);
    }
}