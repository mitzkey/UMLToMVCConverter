namespace UMLToMVCConverter.UMLHelpers
{
    using System.CodeDom;

    public interface IUmlVisibilityMapper
    {
        MemberAttributes UmlToCsharp(string umlVisibility);
    }
}