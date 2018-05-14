namespace UMLToMVCConverter
{
    using UMLToMVCConverter.Models;

    public interface IMvcProjectFilesGenerator
    {
        void GenerateFiles(DataModel dataModel);
    }
}