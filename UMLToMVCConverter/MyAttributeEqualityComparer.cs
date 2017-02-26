using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UMLToMVCConverter
{
    class MyAttributeEqualityComparer : IEqualityComparer<XAttribute>
    {

        public bool Equals(XAttribute x, XAttribute y)
        {
            return (x.Value == y.Value);
        }

        public int GetHashCode(XAttribute obj)
        {
            return obj.GetHashCode();
        }
    }
}
