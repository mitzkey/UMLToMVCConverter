namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IRelationshipFactory
    {
        IRelationship Create(XElement xAssociation, List<ExtendedCodeTypeDeclaration> types);
    }
}