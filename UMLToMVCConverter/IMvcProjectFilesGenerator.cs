namespace UMLToMVCConverter
{
    using UMLToMVCConverter.Domain;

    public interface IMvcProjectFilesGenerator
    {
        void GenerateFiles(DataModel dataModel);
    }
}