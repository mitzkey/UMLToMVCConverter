namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IAssociationFactory
    {
        IRelationship Create(XElement xAssociation, IEnumerable<ExtendedCodeTypeDeclaration> types);
    }
}