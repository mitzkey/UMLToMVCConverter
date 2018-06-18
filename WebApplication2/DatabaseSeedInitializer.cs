using System.Threading.Tasks;

namespace WebApplication2
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using WebApplication2.Models;

    public static class DatabaseSeedInitializer
    {
        public static IWebHost Seed(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                Task.Run(async () =>
                {
                    var context = serviceProvider.GetService<TestowyZKartki01Context>();
                    if (!context.StatusWnioskuSet.Any())
                    {
                        var literals = new List<StatusWniosku>
                        {
							new StatusWniosku { ID = 1, Name = "nierozpatrzony" },
							new StatusWniosku { ID = 2, Name = "wymagaWyjasnienia" },
							new StatusWniosku { ID = 3, Name = "zaakceptowany" },
							new StatusWniosku { ID = 4, Name = "odrzucony" }
                        };
                        context.AddRange(literals);
                        context.SaveChanges();
                    }
                    if (!context.DzienTygodniaSet.Any())
                    {
                        var literals = new List<DzienTygodnia>
                        {
							new DzienTygodnia { ID = 1, Name = "poniedzialek" },
							new DzienTygodnia { ID = 2, Name = "wtorek" },
							new DzienTygodnia { ID = 3, Name = "sroda" }
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