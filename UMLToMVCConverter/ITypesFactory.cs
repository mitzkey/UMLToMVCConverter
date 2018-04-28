namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public interface ITypesFactory
    {
        IEnumerable<ExtendedCodeTypeDeclaration> Create(XElement xUmlModel);
    }
}