namespace UMLToEFConverter.Generators.Deserializers.Interfaces
{
    using System.Xml.Linq;
    using UMLToEFConverter.Models;

    public interface IAssociationDeserializer
    {
        Association Create(XElement xAssociation);
    }
}