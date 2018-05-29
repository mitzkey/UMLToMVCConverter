namespace UMLToMVCConverter.UMLHelpers
{
    using System;
    using UMLToMVCConverter.UMLHelpers.Interfaces;

    public class UmlVisibilityMapper : IUmlVisibilityMapper
    {
        public string UmlToCsharpString(string umlVisibility)
        {
            switch (umlVisibility.ToLower())
            {
                case "public":
                    return CSharpVisibilityString.Public;
                case "private":
                    return CSharpVisibilityString.Private;
                default:
                    throw new NotImplementedException("UML visibility: " + umlVisibility + " mapping not implemented.");
            }
        }
    }
}
