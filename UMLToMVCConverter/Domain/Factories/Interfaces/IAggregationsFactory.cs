namespace UMLToMVCConverter.Domain.Factories.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain.Models;

    public interface IAggregationsFactory
    {
        IEnumerable<Aggregation> Create(XElement xUmlModel);
    }
}