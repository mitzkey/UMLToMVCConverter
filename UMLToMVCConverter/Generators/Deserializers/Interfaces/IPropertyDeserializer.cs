namespace UMLToMVCConverter.Generators.Deserializers.Interfaces
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IPropertyDeserializer
    {
        Property Create(TypeModel type, XElement xProperty);

        Property CreateBasicProperty(string name, Type type, Type genericType = null);
    }
}