namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IEFRelationshipModelFactory
    {
        EFRelationshipModel Create(XElement xAssociation, IEnumerable<ExtendedCodeTypeDeclaration> types);
    }
}