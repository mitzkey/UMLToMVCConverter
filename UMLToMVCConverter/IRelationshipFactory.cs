namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IRelationshipFactory
    {
        IRelationship Create(XElement xAssociation, List<ExtendedCodeTypeDeclaration> types);
    }
}