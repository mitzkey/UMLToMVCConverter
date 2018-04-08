namespace UMLToMVCConverter
{
    public static class Program
    {
        private const string TemporaryHardCodedDiagramPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inzynierska\UMLToMVCConverter\Diagramy\MainDiagram.xml";
        private const string TemporaryHardCodedMvcProjectPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1";
        private const string TemporaryHardCodedConnectionString =
            @"Server=(localdb)\mssqllocaldb;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";
        private const string TemporaryHardCodedMvcBuildFolder =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inzynierska\Converter Workspace";

        public static void Main(string[] args)
        {
            var xmiPath = TemporaryHardCodedDiagramPath;
            var mvcProject = new MvcProject(TemporaryHardCodedMvcProjectPath);
            var dbConnectionString = TemporaryHardCodedConnectionString;
            var mvcProjectBuildFolder = TemporaryHardCodedMvcBuildFolder;

            var mvcProjectConfigurator = new MvcProjectConfigurator(mvcProject, dbConnectionString, mvcProjectBuildFolder);
            var cg = new DataModelGenerator(xmiPath, mvcProjectConfigurator);

            cg.GenerateMvcFiles();
        }
    }
}
