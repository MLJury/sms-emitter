using System;
using System.Collections.Generic;

namespace Kama.SmsService.Core.Model
{
    public class Sms : AppCore.Notification
    { 
        public long ID { get; set; }

        public int Count { get; set; }

        public string RecipientNumber { get; set; }

        public int MessageNumber { get; set; }

        public string Text { get; set; }
    }
}
