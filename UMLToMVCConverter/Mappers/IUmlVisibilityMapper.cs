using System.CodeDom;

namespace UMLToMVCConverter.Mappers
{
    public interface IUmlVisibilityMapper
    {
        MemberAttributes UmlToCsharp(string umlVisibility);
    }
}