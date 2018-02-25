using System;
using System.CodeDom;

namespace UMLToMVCConverter.Mappers
{
    class UmlVisibilityMapper
    {
        public static MemberAttributes UmlToCsharp(string umlVisibility)
        {
            switch (umlVisibility.ToLower())
            {
                case "public":
                    return MemberAttributes.Public;
                case "private":
                    return MemberAttributes.Private;
                default:
                    throw new NotImplementedException("Mapowanie widoczności UML: " + umlVisibility + " nie zaimplementowane.");
            }
        }
    }
}
