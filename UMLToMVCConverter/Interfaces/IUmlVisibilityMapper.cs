namespace UMLToMVCConverter.Interfaces
{
    using System.CodeDom;

    public interface IUmlVisibilityMapper
    {
        MemberAttributes UmlToCsharp(string umlVisibility);
    }
}