using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApplication3.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Testowy02Context>
    {
        public Testowy02Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<Testowy02Context>();

            var connectionString = configuration.GetConnectionString("Testowy02Context");

            builder.UseSqlServer(connectionString);

            return new Testowy02Context(builder.Options);

        }
    }
}