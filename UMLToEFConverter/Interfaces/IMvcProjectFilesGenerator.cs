namespace UMLToEFConverter.Interfaces
{
    using UMLToEFConverter.Models;

    public interface IMvcProjectFilesGenerator
    {
        void GenerateFiles(DataModel dataModel);
    }
}