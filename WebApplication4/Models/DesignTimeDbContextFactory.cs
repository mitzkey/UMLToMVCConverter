using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApplication4.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Testowy03Context>
    {
        public Testowy03Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<Testowy03Context>();

            var connectionString = configuration.GetConnectionString("Testowy03Context");

            builder.UseSqlServer(connectionString);

            return new Testowy03Context(builder.Options);

        }
    }
}