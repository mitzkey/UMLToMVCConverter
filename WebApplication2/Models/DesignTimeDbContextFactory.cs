using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApplication2.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestowyZKartki01Context>
    {
        public TestowyZKartki01Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TestowyZKartki01Context>();

            var connectionString = configuration.GetConnectionString("TestowyZKartki01Context");

            builder.UseSqlServer(connectionString);

            return new TestowyZKartki01Context(builder.Options);

        }
    }
}