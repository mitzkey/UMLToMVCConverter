﻿namespace UMLToMVCConverter.Domain.Models
{
    using System.Collections.Generic;

    public class DataModel
    {
        public IEnumerable<TypeModel> Types { get; set; }

        public IEnumerable<EFRelationship> EFRelationshipModels { get; set; }

        public IEnumerable<Enumeration> EnumerationModels { get; set; }
    }
}