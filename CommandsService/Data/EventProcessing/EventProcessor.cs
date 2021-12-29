using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessors
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetectEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    break;
                default:
                    break;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using(var scoped = _scopeFactory.CreateScope())
            {
                var repo = scoped.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);
                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if(!repo.ExternalPlatformExists(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine($"--> Platform with externalID= {plat.ExternalId} exists");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"==> Platform wasn't added to DB :{ex.Message}");
                }
                
            }
        }
        
        private EventType DetectEvent(string eventMessage)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(eventMessage);
            switch (eventType.Event)
            {
                case "Platform Published":
                    Console.WriteLine("Event Detected ==> Platform publish");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("Event Detection Failed!!");
                    return EventType.Undetermined;
            }
        }
    }
}