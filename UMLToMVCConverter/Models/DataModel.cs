namespace UMLToMVCConverter.Models
{
    using System.Collections.Generic;

    public class DataModel
    {
        public IEnumerable<ExtendedCodeTypeDeclaration> Types { get; set; }

        public IEnumerable<EFRelationshipModel> EFRelationshipModels { get; set; }

        public IEnumerable<EnumerationModel> EnumerationModels { get; set; }
    }
}