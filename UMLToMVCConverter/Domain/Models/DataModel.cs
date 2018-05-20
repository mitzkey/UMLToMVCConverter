namespace UMLToMVCConverter.Domain.Models
{
    using System.Collections.Generic;

    public class DataModel
    {
        public DataModel(
            IEnumerable<TypeModel> types,
            IEnumerable<EFRelationship> efRelationshipModels,
            IEnumerable<Enumeration> enumerationModels)
        {
            this.Types = types ?? new List<TypeModel>();
            this.EFRelationshipModels = efRelationshipModels ?? new List<EFRelationship>();
            this.EnumerationModels = enumerationModels ?? new List<Enumeration>();
        }

        public IEnumerable<TypeModel> Types { get; set; }

        public IEnumerable<EFRelationship> EFRelationshipModels { get; set; }

        public IEnumerable<Enumeration> EnumerationModels { get; set; }
    }
}