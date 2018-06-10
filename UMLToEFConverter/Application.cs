namespace UMLToEFConverter
{
    using System;
    using System.Linq;
    using UMLToEFConverter.Common.XmiTools.Interfaces;
    using UMLToEFConverter.Generators;
    using UMLToEFConverter.Generators.Interfaces;
    using UMLToEFConverter.Interfaces;

    public class Application
    {
        private readonly IDataModelGenerator dataModelGenerator;
        private readonly IMvcProjectFilesGenerator mvcProjectFilesGenerator;
        private readonly IMigrationServiceClient migrationServiceClient;
        private readonly IXmiWrapper xmiWrapper;
        private readonly IProjectPublisher projectPublisher;

        public Application(
            IDataModelGenerator dataModelGenerator,
            IMvcProjectFilesGenerator mvcProjectFilesGenerator,
            IMigrationServiceClient migrationServiceClient,
            IXmiWrapper xmiWrapper,
            IProjectPublisher projectPublisher)
        {
            this.dataModelGenerator = dataModelGenerator;
            this.mvcProjectFilesGenerator = mvcProjectFilesGenerator;
            this.migrationServiceClient = migrationServiceClient;
            this.xmiWrapper = xmiWrapper;
            this.projectPublisher = projectPublisher;
        }

        public void Run()
        {
            var xUmlModel = this.xmiWrapper.GetXUmlModels().Single();

            var dataModel = this.dataModelGenerator.Create(xUmlModel);

            this.mvcProjectFilesGenerator.GenerateFiles(dataModel);

            this.projectPublisher.PublishProject();

            this.migrationServiceClient.AddMigration();

            this.projectPublisher.PublishProject();

            this.migrationServiceClient.RunMigration();

            Console.WriteLine(@"Finished processing project, press any key to continue..");
            Console.ReadLine();
        }
    }
}
