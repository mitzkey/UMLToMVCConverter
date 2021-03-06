﻿namespace UMLToEFConverter.Common.XmiTools
{
    using System.Xml.Linq;
    using UMLToEFConverter.Common.XmiTools.Interfaces;

    public class XAttributeEqualityComparer : IXAttributeEqualityComparer
    {
        public bool Equals(XAttribute x, XAttribute y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(XAttribute obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
