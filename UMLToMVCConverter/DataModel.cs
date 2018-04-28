namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.Interfaces;

    public class DataModel
    {
        public DataModel(IEnumerable<ExtendedCodeTypeDeclaration> types, IEnumerable<IRelationship> associations)
        {
            this.Types = types;
            this.Associations = associations;
        }

        public IEnumerable<ExtendedCodeTypeDeclaration> Types { get; set; }

        public IEnumerable<IRelationship> Associations { get; set; }
    }
}