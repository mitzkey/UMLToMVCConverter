namespace UMLToEFConverter.Generators.Deserializers.Interfaces
{
    using System;
    using System.Xml.Linq;
    using UMLToEFConverter.Models;

    public interface IPropertyDeserializer
    {
        Property Create(TypeModel type, XElement xProperty);

        Property CreateBasicProperty(string name, Type type, Type genericType = null);
    }
}