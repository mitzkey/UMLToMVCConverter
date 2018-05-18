namespace UMLToMVCConverter
{
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;

    public interface IMvcProjectFilesGenerator
    {
        void GenerateFiles(DataModel dataModel);
    }
}