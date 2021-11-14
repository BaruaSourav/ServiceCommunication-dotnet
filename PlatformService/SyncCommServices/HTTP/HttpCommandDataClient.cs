using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncCommServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task SendPlatformToCommand(PlatformReadDto p)
        {
            var httpContent = new StringContent(
              JsonSerializer.Serialize(p),
              Encoding.UTF8,
              "application/json"  
            );

            var response = await _httpClient.PostAsync($"{_config["CommandServiceUrl"]}/api/csrv/platforms/heartbeat", httpContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> HTTP communication PlatformService->>CommandService is up and running");
            }
            else
            {
                Console.WriteLine("--> HTTP communication PlatformService->>CommandService is NOT up and running");
            }
        }
    }
}