namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public class DataModel
    {
        public DataModel(IEnumerable<ExtendedCodeTypeDeclaration> types, IEnumerable<EFRelationshipModel> efRelationshipModels)
        {
            this.Types = types;
            this.EFRelationshipModels = efRelationshipModels;
        }

        public IEnumerable<ExtendedCodeTypeDeclaration> Types { get; set; }

        public IEnumerable<EFRelationshipModel> EFRelationshipModels { get; set; }
    }
}