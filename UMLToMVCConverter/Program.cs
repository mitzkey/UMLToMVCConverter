namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using Autofac;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.Interfaces;
    using UMLToMVCConverter.Mappers;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var xmiPath = args[0];
            var mvcProjectFolderPath = args[1];
            var dbConnectionString = args[2];
            var workspaceFolderPath = args[3];
            var defaultNamespaceName = args[4];

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
            builder.RegisterType<DbContextFactoryClassTextTemplate>().As<IDbContextFactoryClassTextTemplate>().SingleInstance();
            builder.RegisterType<ScriptRunner>().As<IScriptRunner>().SingleInstance();
            builder.RegisterType<DbContextTextTemplate>().As<IDbContextClassTextTemplate>().SingleInstance();
            builder.RegisterType<AttributeNameResolver>().As<IAttributeNameResolver>().SingleInstance();
            builder.RegisterType<AssociationFactory>().As<IAssociationFactory>().SingleInstance();
            builder.RegisterType<TypesFactory>().As<ITypesFactory>().SingleInstance();
            builder.RegisterType<MvcProjectFilesGenerator>().As<IMvcProjectFilesGenerator>().SingleInstance();
            builder.RegisterType<DataModelFactory>().As<IDataModelFactory>().SingleInstance();
            builder.RegisterType<AssociationsFactory>().As<IAssociationsFactory>().SingleInstance();
            builder.RegisterType<RelationshipFactory>().As<IRelationshipFactory>().SingleInstance();

            builder.RegisterType<BasicTypeTextTemplate>().As<IBasicTypeTextTemplate>().InstancePerDependency();
            builder.RegisterType<ModelClassTextTemplate>().As<IModelClassTextTemplate>().InstancePerDependency();

            return builder.Build();
        }
    }
}
