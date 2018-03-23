namespace UMLToMVCConverter
{
    using System.Reflection;

    public interface IProjectBuilder
    {
        Assembly BuildProject(string projectFolderPath);
    }
}