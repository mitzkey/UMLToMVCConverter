namespace UMLToMVCConverter
{
    using Autofac;

    public static class Program
    {
        private const string TemporaryHardCodedDiagramPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inzynierska\UMLToMVCConverter\Diagramy\MainDiagram.xml";
        private const string TemporaryHardCodedMvcProjectPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1";
        private const string TemporaryHardCodedConnectionString =
            @"Server=(localdb)\mssqllocaldb;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";
        private const string TemporaryHardCodedWorkFolder =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inzynierska\Converter Workspace";

        public static void Main(string[] args)
        {
            var xmiPath = TemporaryHardCodedDiagramPath;
            var mvcProjectFolderPath = TemporaryHardCodedMvcProjectPath;
            var dbConnectionString = TemporaryHardCodedConnectionString;
            var workspaceFolderPath = TemporaryHardCodedWorkFolder;

            var container = ConfigureContainer(xmiPath, mvcProjectFolderPath, dbConnectionString, workspaceFolderPath);

            var mvcProjectConfigurator = new MvcProjectConfigurator(mvcProject, dbConnectionString, mvcProjectBuildFolder);
            var cg = new DataModelGenerator(xmiPath, mvcProjectConfigurator);

            cg.GenerateMvcFiles();
        }

        private static IContainer ConfigureContainer(
            string xmiPath,
            string mvcProjectFolderPath,
            string dbConnectionString,
            string workspaceFolderPath)
        {
            var builder = new ContainerBuilder();

            var mvcProject = new MvcProject(mvcProjectFolderPath);
            builder.RegisterInstance(mvcProject).As<IMvcProject>().SingleInstance();

            builder.RegisterType<Application>().AsSelf().SingleInstance();
            builder.RegisterType<DataModelGenerator>().As<IDataModelGenerator>().SingleInstance();
            builder.RegisterType<MvcProjectConfigurator>().As<IMvcProjectConfigurator>().SingleInstance();
            builder.RegisterType<ProjectBuilder>().As<IProjectBuilder>().SingleInstance();
            builder.RegisterType<StartupCsConfigurator>().As<IStartupCsConfigurator>().SingleInstance();
            builder.RegisterType<UmlTypesHelper>().As<IUmlTypesHelper>().SingleInstance();
            builder.RegisterType<XmiWrapper>().As<IXmiWrapper>().SingleInstance();

            return builder.Build();
        }
    }
}
