namespace UMLToMVCConverter
{
    public static class Program
    {
        private const string TemporaryHardCodedDiagramPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inżynierska\UMLToMVCConverter\Diagramy\MainDiagram.xml";
        private const string TemporaryHardCodedMvcProjectPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1";
        private const string TemporaryHardCodedConnectionString =
            @"Server=(localdb)\mssqllocaldb;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static void Main(string[] args)
        {
            var xmiPath = TemporaryHardCodedDiagramPath;
            var mvcProjectFolderPath = TemporaryHardCodedMvcProjectPath;
            var dbConnectionString = TemporaryHardCodedConnectionString;

            var mvcProjectConfigurator = new MvcProjectConfigurator(mvcProjectFolderPath, dbConnectionString);
            var cg = new DataModelGenerator(xmiPath, mvcProjectConfigurator);

            cg.GenerateMvcFiles();
        }
    }
}
