namespace UMLToMVCConverter
{
    using System;
    using System.Linq;
    using UMLToMVCConverter.Interfaces;

    public class Application
    {
        private readonly IDataModelFactory dataModelFactory;
        private readonly IMvcProjectFilesGenerator mvcProjectFilesGenerator;
        private readonly IMigrationServiceClient migrationServiceClient;
        private readonly IXmiWrapper xmiWrapper;
        private readonly IProjectPublisher projectPublisher;

        public Application(
            IDataModelFactory dataModelFactory,
            IMvcProjectFilesGenerator mvcProjectFilesGenerator,
            IMigrationServiceClient migrationServiceClient,
            IXmiWrapper xmiWrapper,
            IProjectPublisher projectPublisher)
        {
            this.dataModelFactory = dataModelFactory;
            this.mvcProjectFilesGenerator = mvcProjectFilesGenerator;
            this.migrationServiceClient = migrationServiceClient;
            this.xmiWrapper = xmiWrapper;
            this.projectPublisher = projectPublisher;
        }

        public void Run()
        {
            var xUmlModel = this.xmiWrapper.GetXUmlModels().Single();

            var dataModel = this.dataModelFactory.Create(xUmlModel);

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
