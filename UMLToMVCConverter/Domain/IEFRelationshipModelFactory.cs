﻿namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

    public interface IEFRelationshipModelFactory
    {
        IEnumerable<EFRelationshipModel> Create(IEnumerable<Aggregation> aggregations);
    }
}