namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToMVCConverter.Models;

    public interface IAggregationsFactory
    {
        IEnumerable<Aggregation> Create(XElement xUmlModel);
    }
}