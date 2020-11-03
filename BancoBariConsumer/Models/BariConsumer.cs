using Common;
using MassTransit;
using System.Threading.Tasks;

namespace BancoBariConsumer.Models
{

    public class BariConsumer : IConsumer<Message>
    {
        
        public async Task Consume(ConsumeContext<Message> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
            
        }

    }
}
