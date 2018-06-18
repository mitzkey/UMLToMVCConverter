using System.Threading.Tasks;

namespace WebApplication3
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using WebApplication3.Models;

    public static class DatabaseSeedInitializer
    {
        public static IWebHost Seed(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                Task.Run(async () =>
                {
                    var context = serviceProvider.GetService<Testowy02Context>();
                    if (!context.StatusEdycjiSet.Any())
                    {
                        var literals = new List<StatusEdycji>
                        {
							new StatusEdycji { ID = 1, Name = "planowana" },
							new StatusEdycji { ID = 2, Name = "aktualna" },
							new StatusEdycji { ID = 3, Name = "archiwalna" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.StatusZgloszeniaSet.Any())
                    {
                        var literals = new List<StatusZgloszenia>
                        {
							new StatusZgloszenia { ID = 1, Name = "do rozpatrzenia" },
							new StatusZgloszenia { ID = 2, Name = "przyjete" },
							new StatusZgloszenia { ID = 3, Name = "odrzucone" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.TypJednostkiSet.Any())
                    {
                        var literals = new List<TypJednostki>
                        {
							new TypJednostki { ID = 1, Name = "uczelnia wyzsza" },
							new TypJednostki { ID = 2, Name = "wydzial" },
							new TypJednostki { ID = 3, Name = "instytut" },
							new TypJednostki { ID = 4, Name = "katedra" },
							new TypJednostki { ID = 5, Name = "instytutBadawczy" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.TypJednostkiOrganizacyjnejSet.Any())
                    {
                        var literals = new List<TypJednostkiOrganizacyjnej>
                        {
							new TypJednostkiOrganizacyjnej { ID = 1, Name = "firma" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.StatusRecenzjiSet.Any())
                    {
                        var literals = new List<StatusRecenzji>
                        {
							new StatusRecenzji { ID = 1, Name = "wersjRobocza" },
							new StatusRecenzji { ID = 2, Name = "zatwierdzona" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.StatusPropozycjiSet.Any())
                    {
                        var literals = new List<StatusPropozycji>
                        {
							new StatusPropozycji { ID = 1, Name = "niezatwierdzona" },
							new StatusPropozycji { ID = 2, Name = "zatwierdzona" },
							new StatusPropozycji { ID = 3, Name = "przeslanaProsba" },
							new StatusPropozycji { ID = 4, Name = "przyjeta" },
							new StatusPropozycji { ID = 5, Name = "odrzucona" },
							new StatusPropozycji { ID = 6, Name = "brakOdpowiedzi" }
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