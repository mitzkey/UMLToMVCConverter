namespace UMLToEFConverter
{
    using System.Xml.Linq;
    using Autofac;
    using UMLToEFConverter.CodeTemplates;
    using UMLToEFConverter.CodeTemplates.Interfaces;
    using UMLToEFConverter.Common;
    using UMLToEFConverter.Common.XmiTools;
    using UMLToEFConverter.Common.XmiTools.Interfaces;
    using UMLToEFConverter.Generators;
    using UMLToEFConverter.Generators.Deserializers;
    using UMLToEFConverter.Generators.Deserializers.Interfaces;
    using UMLToEFConverter.Generators.Interfaces;
    using UMLToEFConverter.Interfaces;
    using UMLToEFConverter.Models;
    using UMLToEFConverter.Models.Repositories;
    using UMLToEFConverter.Models.Repositories.Interfaces;
    using UMLToEFConverter.UMLHelpers;
    using UMLToEFConverter.UMLHelpers.Interfaces;
    using IEFRelationshipModelGenerator = UMLToEFConverter.Generators.Interfaces.IEFRelationshipModelGenerator;

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
            builder.RegisterInstance(mvcProject).AsSelf().SingleInstance();

            var xmiDocument = XDocument.Load(xmiPath);
            builder.RegisterInstance(xmiDocument).As<XDocument>().SingleInstance();

            builder.RegisterType<Application>().AsSelf().SingleInstance();
            builder.RegisterType<ProjectPublisher>().As<IProjectPublisher>().SingleInstance();
            builder.RegisterType<StartupCsConfigurator>().As<IStartupCsConfigurator>().SingleInstance();
            builder.RegisterType<MigrationServiceClient>().As<IMigrationServiceClient>().SingleInstance();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterType<ScriptRunner>().As<IScriptRunner>().SingleInstance();

            builder.RegisterType<XAttributeNameResolver>().As<IXAttributeNameResolver>().SingleInstance();
            builder.RegisterType<XAttributeEqualityComparer>().As<IXAttributeEqualityComparer>().SingleInstance();
            builder.RegisterType<XmiWrapper>().As<IXmiWrapper>().SingleInstance();

            builder.RegisterType<UmlTypesHelper>().As<IUmlTypesHelper>().SingleInstance();
            builder.RegisterType<UmlVisibilityMapper>().As<IUmlVisibilityMapper>().SingleInstance();

            builder.RegisterType<EnumerationModelsGenerator>().As<IEnumerationModelsGenerator>().SingleInstance();
            builder.RegisterType<AssociationDeserializer>().As<IAssociationDeserializer>().SingleInstance();
            builder.RegisterType<PropertyDeserializer>().As<IPropertyDeserializer>().SingleInstance();

            builder.RegisterType<AssociationsRepository>().As<IAssociationsRepository>().SingleInstance();
            builder.RegisterType<TypesRepository>().As<ITypesRepository>().SingleInstance();

            builder.RegisterType<AssociationsGenerator>().As<IAssociationsGenerator>().SingleInstance();
            builder.RegisterType<AssociationsForeignKeyGenerator>().As<IAssociationsForeignKeyGenerator>().SingleInstance();
            builder.RegisterType<ForeignKeysGenerator>().As<IForeignKeysGenerator>().SingleInstance();
            builder.RegisterType<NavigationalPropertiesGenerator>().As<INavigationalPropertiesGenerator>().SingleInstance();
            builder.RegisterType<TypesGenerator>().As<ITypesGenerator>().SingleInstance();
            builder.RegisterType<MvcProjectFilesGenerator>().As<IMvcProjectFilesGenerator>().SingleInstance();
            builder.RegisterType<DataModelGenerator>().As<IDataModelGenerator>().SingleInstance();
            builder.RegisterType<EFRelationshipModelGenerator>().As<IEFRelationshipModelGenerator>().SingleInstance();

            builder.RegisterType<BasicTypeTextTemplate>().As<IBasicTypeTextTemplate>().InstancePerDependency();
            builder.RegisterType<ModelClassTextTemplate>().As<IModelClassTextTemplate>().InstancePerDependency();
            builder.RegisterType<MigrationsManagerClassTextTemplate>().As<IMigrationsManagerClassTextTemplate>().SingleInstance();
            builder.RegisterType<DbContextFactoryClassTextTemplate>().As<IDbContextFactoryClassTextTemplate>().SingleInstance();
            builder.RegisterType<ProgramCsTextTemplate>().As<IProgramCsTextTemplate>().SingleInstance();
            builder.RegisterType<DatabaseSeedInitializerTextTemplate>().As<IDatabaseSeedInitializerTextTemplate>().SingleInstance();
            builder.RegisterType<DbContextTextTemplate>().As<IDbContextClassTextTemplate>().SingleInstance();

            return builder.Build();
        }
    }
}
