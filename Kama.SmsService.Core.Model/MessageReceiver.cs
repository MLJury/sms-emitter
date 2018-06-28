using System;
using System.Collections.Generic;

namespace Kama.SmsService.Core.Model
{
    public class MessageReceiver : Message
    {
        public Guid MessageID { get; set; }
        public string ReceiverNumber { get; set; }
        public Boolean IsSent { get; set; }
        public DateTime? SendDate { get; set; }
        public Boolean IsQueue { get; set; }
        public DateTime? QueueDate { get; set; }
    }
}
