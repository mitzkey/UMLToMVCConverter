using System.Threading.Tasks;

namespace WebApplication4
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using WebApplication4.Models;

    public static class DatabaseSeedInitializer
    {
        public static IWebHost Seed(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                Task.Run(async () =>
                {
                    var context = serviceProvider.GetService<Testowy03Context>();
                    if (!context.TrybSet.Any())
                    {
                        var literals = new List<Tryb>
                        {
							new Tryb { ID = 1, Name = "demonstracyjny" },
							new Tryb { ID = 2, Name = "wRealizacji" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.WidokSet.Any())
                    {
                        var literals = new List<Widok>
                        {
							new Widok { ID = 1, Name = "kolowy" },
							new Widok { ID = 2, Name = "skalePoziome" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                }).Wait();
            }
            return host;
        }
    }
}