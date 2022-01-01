using System;
using System.Collections.Generic;
using CommandsService.Models;
using CommandsService.Repositories;
using CommandsService.SyncCommServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.Data
{
    public static class InitDb
    {
        public static void InitPlatforms(IApplicationBuilder appBuilder)
        {
            using(var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformClient>();
                var platforms = grpcClient.ReturnAllPlatforms();
                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
            }
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("Bringing in platforms from platfrom service...");
            foreach (var platform in platforms)
            {
                if(!repo.PlatformExists(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                }
                repo.SaveChanges();
            }
        }
    }
}