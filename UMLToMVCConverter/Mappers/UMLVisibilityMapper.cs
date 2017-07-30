using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace UMLToMVCConverter.Mappers
{
    class UMLVisibilityMapper
    {
        public static MemberAttributes UMLToCsharp(string UMLVis)
        {
            switch (UMLVis.ToLower())
            {
                case "public":
                    return MemberAttributes.Public;
                case "private":
                    return MemberAttributes.Private;
                default:
                    throw new NotImplementedException("Mapowanie widoczności UML: " + UMLVis + " nie zaimplementowane.");
            }
        }
    }
}
