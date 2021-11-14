using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncCommServices.Http{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto p);
    }
}