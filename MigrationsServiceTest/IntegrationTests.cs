using Xunit;

namespace MigrationsServiceTest
{
    using System.IO;
    using MigrationsService;

    public class IntegrationTests
    {
        private string mvcProjectName;
        private string dbContextName;
        private string migrationsNamespace;
        private string mvcProjectPath;
        private string mvcProjectAssemblyPath;

        private MigrationsService sut;

        public IntegrationTests()
        {
            this.mvcProjectPath = @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1\WebApplication1.csproj";
            this.dbContextName = "DefaultContext";
            this.mvcProjectName = "WebApplication1";
            this.migrationsNamespace = "WebApplication1";
            this.mvcProjectAssemblyPath =
                @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1\bin\Debug\netcoreapp2.0\WebApplication1.dll";

            this.sut = new MigrationsService(this.mvcProjectPath, this.mvcProjectName, this.dbContextName, this.migrationsNamespace, this.mvcProjectAssemblyPath);
        }

        [Fact]
        public void Test1()
        {
            // Arrange & Act
            var result = this.sut.AddMigration();

            // Assert
            Assert.True(File.Exists(result));
        }
    }
}
