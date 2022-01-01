using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.SyncCommServices
{
    public interface IPlatformClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}