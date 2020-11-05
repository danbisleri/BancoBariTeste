using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoBariSender.Models
{
    public class MessageListModel
    {
        public static List<Message> listMessage = new List<Message>();


        public static void AddInList(Message message)
        {
            listMessage.Add(message);
        }

        public static List<Message> GetListMessage()
        {
            return listMessage;
        }
    }
}
