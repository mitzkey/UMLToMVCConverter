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

        public Application(
            IDataModelFactory dataModelFactory,
            IMvcProjectFilesGenerator mvcProjectFilesGenerator,
            IMigrationServiceClient migrationServiceClient,
            IXmiWrapper xmiWrapper)
        {
            this.dataModelFactory = dataModelFactory;
            this.mvcProjectFilesGenerator = mvcProjectFilesGenerator;
            this.migrationServiceClient = migrationServiceClient;
            this.xmiWrapper = xmiWrapper;
        }

        public void Run()
        {
            var xUmlModel = this.xmiWrapper.GetXUmlModels().Single();

            var dataModel = this.dataModelFactory.Create(xUmlModel);

            this.mvcProjectFilesGenerator.GenerateFiles(dataModel);

            this.migrationServiceClient.AddMigration();

            this.migrationServiceClient.RunMigration();

            Console.WriteLine(@"Finished processing project, press any key to continue..");
            Console.ReadLine();
        }
    }
}
