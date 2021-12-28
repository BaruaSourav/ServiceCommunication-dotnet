using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/csrv/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [Route("heartbeat")]
        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("--> Command Service : Inbound Post works!");
            return Ok("Command Service Works!");
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine(" --> Reading platforms from CommandsService");

            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

    }
}