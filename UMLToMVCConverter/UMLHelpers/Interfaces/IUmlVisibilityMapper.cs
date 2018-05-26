namespace UMLToMVCConverter.UMLHelpers.Interfaces
{
    using System.CodeDom;

    public interface IUmlVisibilityMapper
    {
        MemberAttributes UmlToCsharp(string umlVisibility);
        string UmlToCsharpString(string umlVisibility);
    }
}