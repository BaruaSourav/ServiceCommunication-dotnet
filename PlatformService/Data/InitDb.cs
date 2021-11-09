using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class InitDb
    {
        public static void PopulateDb(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("==> Platform DB Empty: Seeding Data ");
                context.Platforms.AddRange(
                    new Platform(){Name=".NET", Publisher="Microsoft", Cost= 0.0m, InstalledBy="Sourav"},
                    new Platform(){Name="Docker", Publisher="Docker", Cost= 0.0m, InstalledBy="Sourav"},
                    new Platform(){Name="Python", Publisher="Python Software Foundation (PSF)", Cost= 0.0m, InstalledBy="Miniconda"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("==> There are already some data in Platform DB, skipping seeding");
            }
        }

    }
}