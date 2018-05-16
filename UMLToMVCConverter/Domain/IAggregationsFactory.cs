namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface IAggregationsFactory
    {
        IEnumerable<Aggregation> Create(XElement xUmlModel);
    }
}