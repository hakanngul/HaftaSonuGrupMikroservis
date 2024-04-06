using System.Runtime.InteropServices.ComTypes;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace MicroserviceOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IPublishEndpoint publishEndpoint) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser()
        {
            var userCreatedEvent = new UserCreatedEvent
            {
                UserId = Guid.NewGuid(),
                Phone = "444 5555 55 55",
                Email = "ahmet@outlook.com"
            };

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));


            publishEndpoint.Publish(userCreatedEvent, x => { x.SetAwaitAck(true); }, cancellationTokenSource.Token)
                .ContinueWith(x => { }, cancellationTokenSource.Token);

            return Ok();
        }
    }
}