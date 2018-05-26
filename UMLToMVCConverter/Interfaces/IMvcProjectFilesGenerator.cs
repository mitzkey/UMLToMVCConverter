namespace UMLToMVCConverter.Interfaces
{
    using UMLToMVCConverter.Domain.Models;

    public interface IMvcProjectFilesGenerator
    {
        void GenerateFiles(DataModel dataModel);
    }
}