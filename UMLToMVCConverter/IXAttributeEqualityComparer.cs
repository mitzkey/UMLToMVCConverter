using System.Xml.Linq;

namespace UMLToMVCConverter
{
    using System.Collections.Generic;

    public interface IXAttributeEqualityComparer : IEqualityComparer<XAttribute>
    {
        bool Equals(XAttribute x, XAttribute y);
        int GetHashCode(XAttribute obj);
    }
}