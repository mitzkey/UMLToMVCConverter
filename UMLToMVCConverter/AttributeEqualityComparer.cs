using System.Collections.Generic;
using System.Xml.Linq;

namespace UMLToMVCConverter
{
    public class AttributeEqualityComparer : IEqualityComparer<XAttribute>
    {
        public bool Equals(XAttribute x, XAttribute y)
        {
            return (x.Value == y.Value);
        }

        public int GetHashCode(XAttribute obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
