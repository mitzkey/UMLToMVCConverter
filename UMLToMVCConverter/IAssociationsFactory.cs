namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.Interfaces;

    public interface IAssociationsFactory
    {
        IEnumerable<IRelationship> Create(XElement xUmlModel, IEnumerable<ExtendedCodeTypeDeclaration> types);
    }
}