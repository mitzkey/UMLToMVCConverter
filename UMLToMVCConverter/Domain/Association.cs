namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Association
    {
        public Association(IEnumerable<XElement> associationEndsXElements)
        {
            this.EndsXElements = associationEndsXElements.ToList();
        }

        public List<XElement> EndsXElements { get; private set; }
    }
}