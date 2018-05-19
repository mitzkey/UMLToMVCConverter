namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Models;

    public interface IPropertyFactory
    {
        Property Create(TypeModel type, XElement xProperty);

        Property CreateBasicProperty(string name, Type type, Type genericType = null);
    }
}