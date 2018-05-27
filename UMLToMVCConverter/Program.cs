namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using Autofac;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.CodeTemplates.Interfaces;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Factories;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Generators;
    using UMLToMVCConverter.Domain.Generators.Interfaces;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.Domain.Repositories;
    using UMLToMVCConverter.Domain.Repositories.Interfaces;
    using UMLToMVCConverter.Interfaces;
    using UMLToMVCConverter.UMLHelpers;
    using UMLToMVCConverter.UMLHelpers.Interfaces;
    using UMLToMVCConverter.XmiTools;
    using UMLToMVCConverter.XmiTools.Interfaces;

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
            builder.RegisterType<XAttributeNameResolver>().As<IXAttributeNameResolver>().SingleInstance();
            builder.RegisterType<TypesGenerator>().As<ITypesGenerator>().SingleInstance();
            builder.RegisterType<MvcProjectFilesGenerator>().As<IMvcProjectFilesGenerator>().SingleInstance();
            builder.RegisterType<DataModelFactory>().As<IDataModelFactory>().SingleInstance();
            builder.RegisterType<EFRelationshipModelFactory>().As<IEFRelationshipModelFactory>().SingleInstance();
            builder.RegisterType<ForeignKeysGenerator>().As<IForeignKeysGenerator>().SingleInstance();
            builder.RegisterType<NavigationalPropertiesGenerator>().As<INavigationalPropertiesGenerator>().SingleInstance();
            builder.RegisterType<PropertyFactory>().As<IPropertyFactory>().SingleInstance();
            builder.RegisterType<DatabaseSeedInitializerTextTemplate>().As<IDatabaseSeedInitializerTextTemplate>().SingleInstance();
            builder.RegisterType<EnumerationModelsFactory>().As<IEnumerationModelsFactory>().SingleInstance();
            builder.RegisterType<ProgramCsTextTemplate>().As<IProgramCsTextTemplate>().SingleInstance();
            builder.RegisterType<TypesRepository>().As<ITypesRepository>().SingleInstance();
            builder.RegisterType<AssociationFactory>().As<IAssociationFactory>().SingleInstance();
            builder.RegisterType<AssociationsForeignKeyGenerator>().As<IAssociationsForeignKeyGenerator>().SingleInstance();
            builder.RegisterType<AssociationsRepository>().As<IAssociationsRepository>().SingleInstance();
            builder.RegisterType<AssociationsGenerator>().As<IAssociationsGenerator>().SingleInstance();

            builder.RegisterType<BasicTypeTextTemplate>().As<IBasicTypeTextTemplate>().InstancePerDependency();
            builder.RegisterType<ModelClassTextTemplate>().As<IModelClassTextTemplate>().InstancePerDependency();

            return builder.Build();
        }
    }
}
