namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using Autofac;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.Mappers;

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
        private const string TemporaryHardCodedDefaultNamespaceName =
            @"Default";

        public static void Main(string[] args)
        {
            var xmiPath = TemporaryHardCodedDiagramPath;
            var mvcProjectFolderPath = TemporaryHardCodedMvcProjectPath;
            var dbConnectionString = TemporaryHardCodedConnectionString;
            var workspaceFolderPath = TemporaryHardCodedWorkFolder;
            var defaultNamespaceName = TemporaryHardCodedDefaultNamespaceName;

            var container = ConfigureContainer(
                xmiPath,
                mvcProjectFolderPath,
                dbConnectionString,
                workspaceFolderPath,
                defaultNamespaceName);

            var application = container.Resolve<Application>();

            application.Run();
        }

        private static IContainer ConfigureContainer(
            string xmiPath,
            string mvcProjectFolderPath,
            string dbConnectionString,
            string workspaceFolderPath,
            string defaultNamespaceName)
        {
            var builder = new ContainerBuilder();

            var mvcProject = new MvcProject(
                mvcProjectFolderPath,
                defaultNamespaceName,
                workspaceFolderPath,
                dbConnectionString);
            builder.RegisterInstance(mvcProject).As<IMvcProject>().SingleInstance();

            var xmiDocument = XDocument.Load(xmiPath);
            builder.RegisterInstance(xmiDocument).As<XDocument>().SingleInstance();

            builder.RegisterType<Application>().AsSelf().SingleInstance();
            builder.RegisterType<DataModelGenerator>().As<IDataModelGenerator>().SingleInstance();
            builder.RegisterType<MvcProjectConfigurator>().As<IMvcProjectConfigurator>().SingleInstance();
            builder.RegisterType<ProjectPublisher>().As<IProjectPublisher>().SingleInstance();
            builder.RegisterType<StartupCsConfigurator>().As<IStartupCsConfigurator>().SingleInstance();
            builder.RegisterType<UmlTypesHelper>().As<IUmlTypesHelper>().SingleInstance();
            builder.RegisterType<XmiWrapper>().As<IXmiWrapper>().SingleInstance();
            builder.RegisterType<UmlVisibilityMapper>().As<IUmlVisibilityMapper>().SingleInstance();
            builder.RegisterType<XAttributeEqualityComparer>().As<IXAttributeEqualityComparer>().SingleInstance();
            builder.RegisterType<MigrationServiceClient>().As<IMigrationServiceClient>().SingleInstance();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterType<MigrationsManagerClassTextTemplate>().As<IMigrationsManagerClassTextTemplate>().SingleInstance();

            return builder.Build();
        }
    }
}
