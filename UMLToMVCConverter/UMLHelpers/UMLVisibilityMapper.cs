namespace UMLToMVCConverter.UMLHelpers
{
    using System;
    using System.CodeDom;
    using UMLToMVCConverter.Interfaces;

    public class UmlVisibilityMapper : IUmlVisibilityMapper
    {
        public MemberAttributes UmlToCsharp(string umlVisibility)
        {
            switch (umlVisibility.ToLower())
            {
                case "public":
                    return MemberAttributes.Public;
                case "private":
                    return MemberAttributes.Private;
                default:
                    throw new NotImplementedException("UML visibility: " + umlVisibility + " mapping not implemented.");
            }
        }
    }
}
