namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IAssociationsFactory
    {
        IEnumerable<IRelationship> Create(XElement xUmlModel, IEnumerable<ExtendedCodeTypeDeclaration> types);
    }
}