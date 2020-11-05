using Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BancoBariSender.Models
{

    public class BariQueue : IBariQueue
    {
        private readonly IConfiguration _configuration;
        
        public BariQueue(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void AddQueue(Message message = null)
        {
            try
            {
                if(message == null)
                    message = new Message(Dns.GetHostName(), "Hello World!");

                var host = _configuration.GetSection("QueueHost").Value;
                var factory = new ConnectionFactory()
                {
                    HostName = host,
                    UserName = "guest",
                    Password = "guest",
                    RequestedHeartbeat = TimeSpan.FromMinutes(3),
                    Port = AmqpTcpEndpoint.UseDefaultPort
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "bariQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                        channel.BasicPublish(exchange: "",
                            routingKey: "bariQueue",
                            basicProperties: null,
                            body: body
                            );

                        MessageListModel.AddInList(message);
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //AddQueue();
            }
            
        }
    }
}
