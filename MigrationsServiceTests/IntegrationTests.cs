namespace MigrationsServiceTests
{
    using System.IO;
    using MigrationsService;
    using NUnit.Framework;

    public class IntegrationTests
    {
        private string mvcProjectName;
        private string dbContextName;
        private string migrationsNamespace;
        private string mvcProjectPath;
        private string mvcProjectAssemblyPath;

        private MigrationsService sut;

        [SetUp]
        public void Setup()
        {
            this.mvcProjectPath = @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1\WebApplication1.csproj";
            this.dbContextName = "DefaultContext";
            this.mvcProjectName = "WebApplication1";
            this.migrationsNamespace = "WebApplication1";
            this.mvcProjectAssemblyPath =
                @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1\bin\Debug\netcoreapp2.0\WebApplication1.dll";

            this.sut = new MigrationsService(this.mvcProjectPath, this.mvcProjectName, this.dbContextName, this.migrationsNamespace, this.mvcProjectAssemblyPath);
        }

        [Test]
        public void Migration_Is_Added_Successfully()
        {
            // Arrange & Act
            var result = this.sut.AddMigration();

            // Assert
            Assert.That(File.Exists(result));
        }
    }
}