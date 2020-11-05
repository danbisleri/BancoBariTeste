using Microsoft.Extensions.Configuration;
using BancoBariSender.Models;
using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BancoBariSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SendMessageController(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<IActionResult> SendMessage(MessageAPI messageApi)
        {
            if (messageApi != null)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetService<IBariQueue>();
                        var message = new Message(messageApi.Host, messageApi.Description);
                        service.AddQueue(message);
                    }

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
             return BadRequest();
           
        }
    }
}
