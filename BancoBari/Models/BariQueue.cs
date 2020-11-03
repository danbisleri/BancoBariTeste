using Common;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BancoBariSender.Models
{

    public class BariQueue: BackgroundService
    {
        private readonly ILogger<BariQueue> _logger;
        private readonly IBus _eventBus;

        public BariQueue(ILogger<BariQueue> logger, IBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Message> listMessage = new List<Message>();

            while (true)
            {
                Message message = new Message(_eventBus.Address.ToString(), "Hello World!");

                Uri uri = new Uri("rabbitmq://127.0.0.1/bariQueue");

                var endPoint = await _eventBus.GetSendEndpoint(uri);

                _logger.LogInformation($"[x] Mensagem enviada [x] \n {JsonConvert.SerializeObject(message)}");

                await endPoint.Send(message);

                await Task.Delay(5000, stoppingToken);

                
            }
        }
    }
}
