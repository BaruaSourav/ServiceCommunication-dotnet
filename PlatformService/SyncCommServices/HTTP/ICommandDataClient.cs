using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncCommServices.HttpGet{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto p);
    }
}