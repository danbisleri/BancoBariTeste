using System;

namespace Common
{
    public class Message
    {
        public Message(string host, string message)
        {
            Id = Guid.NewGuid();
            Host = host;
            Description = message;
            DataEnvio = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string Description { get; set; }
        public DateTime DataEnvio { get; set; }

    }
}
