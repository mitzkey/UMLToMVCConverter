namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public class DataModel
    {
        public IEnumerable<ExtendedCodeTypeDeclaration> Types { get; set; }

        public IEnumerable<EFRelationshipModel> EFRelationshipModels { get; set; }

        public IEnumerable<EnumerationModel> EnumerationModels { get; set; }
    }
}