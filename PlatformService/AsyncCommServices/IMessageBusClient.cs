using PlatformService.Dtos;

namespace PlatformService.AsyncCommServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishDto platPublishDto);
    }
}