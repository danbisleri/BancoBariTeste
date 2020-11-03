using Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BancoBariConsumer.Models
{

    public class BariConsumer : IBariConsumer
    {
        private readonly IConfiguration _configuration;

        public BariConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Consumer()
        {
            var host = _configuration.GetSection("QueueHost").Value;
            try
            {
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
                        while (channel.IsOpen)
                        {
                            Thread.Sleep(1);
                            channel.QueueDeclare(queue: "bariQueue",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null
                              );


                            var consumer = new EventingBasicConsumer(channel);

                            string message = "";

                            consumer.Received += (model, ea) =>
                            {
                                var body = ea.Body.ToArray();
                                message = Encoding.UTF8.GetString(body);
                                //obj = JsonConvert.DeserializeObject<Message>(message);
                            };

                            channel.BasicConsume(queue: "bariQueue",
                                autoAck: true,
                                consumer: consumer
                                );
                        }
                    }
                    connection.Close();
                }
            }
            catch
            {

            }
        }


    }
}
