using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncCommServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly IPlatformRepo _repository;
        public IMapper _mapper { get; }
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient
            )
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("==> Getting list of Platforms");
            var platformList = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformList));
        }

        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine($"==> Getting platform of id {id}");
            var platform = _repository.GetPlatformById(id);
            if(platform != null)
            {
                Console.WriteLine($"==> Found! Returning platform of id {id}");
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            Console.WriteLine("==> Not Found!");
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatformAsync(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            Console.WriteLine("==> Platform Created");
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> HTTP Sync communication was unsuccessful. Message: {ex.Message}");
            }
            
            return CreatedAtRoute(nameof(GetPlatformById),new {Id = platformReadDto.Id}, platformReadDto);
        }

        [HttpGet("delete/{id}", Name="DeletePlatformById")]
        public ActionResult DeletePlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if(platform != null)
            {
                Console.WriteLine($"==> Found! Deleting platform of id {id}");
                _repository.DeletePlatformById(id);
                _repository.SaveChanges();
                return Ok(new {message =$"Platform with id = {id} Deleted"});
            }
            Console.WriteLine("==> Not Found!");
            return NotFound();
        }
    }
}