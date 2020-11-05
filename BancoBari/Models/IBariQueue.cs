using Common;

namespace BancoBariSender.Models
{
    public interface IBariQueue
    {
        void AddQueue(Message message = null);
    }
}