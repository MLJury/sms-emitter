using System;
using System.Collections.Generic;

namespace Kama.SmsService.Core.Model
{
    public class SendTry : Model
    {
        public Guid ReceiverMessageID { get; set; }
        public DateTime Date { get; set; }
        public bool Succeed { get; set; }
        public string Message { get; set; }
    }
}
