using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandsService.SyncCommServices
{
    public class PlatformClient : IPlatformClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PlatformClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"==> GRPC Communication to: {_config["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_config["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var resp = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(resp.Platform);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"==> GRPC Server communication failed..!! : {ex.Message}");
                return null;
            }
        }
    }
}