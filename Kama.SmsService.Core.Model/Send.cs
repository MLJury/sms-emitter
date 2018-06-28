using System;
using System.Collections.Generic;

namespace Kama.SmsService.Core.Model
{
    public class Send : Model
    {
        public Guid ReceiverMessageID { get; set; }
        public bool IsSent { get; set; }
        public string Message { get; set; }
    }
}
