using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/csrv/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        public PlatformsController()
        {
            
        }
        [Route("heartbeat")]
        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("--> Command Service : Inbound Post works!");
            return Ok("Command Service Works!");
        }

    }
}