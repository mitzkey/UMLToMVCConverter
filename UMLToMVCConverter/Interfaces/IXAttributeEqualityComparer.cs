namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface IXAttributeEqualityComparer : IEqualityComparer<XAttribute>
    {
        bool Equals(XAttribute x, XAttribute y);
        int GetHashCode(XAttribute obj);
    }
}