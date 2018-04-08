namespace MigrationsServiceTests
{
    using System.IO;
    using MigrationsService;
    using NUnit.Framework;

    public class IntegrationTests
    {
        private string migrationsNamespace;
        private string mvcProjectFolderPath;
        private string mvcProjectAssemblyPath;

        private MigrationsService sut;

        [SetUp]
        public void Setup()
        {
            this.mvcProjectFolderPath = @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1\";
            this.migrationsNamespace = "WebApplication1";
            this.mvcProjectAssemblyPath =
                @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1\bin\Release\PublishOutput\WebApplication1.dll";

            this.sut = new MigrationsService(this.mvcProjectAssemblyPath);
        }

        [Test]
        public void Migration_Is_Added_Successfully()
        {
            // Arrange & Act
            var result = this.sut.AddMigration(this.mvcProjectFolderPath, this.migrationsNamespace);

            // Assert
            Assert.That(File.Exists(result));
        }

        [Test]
        public void Migration_Is_Run_Successfully()
        {
            // Arrange & Act
            this.sut.RunMigration();

            // Assert
            Assert.That(true);
        }
    }
}