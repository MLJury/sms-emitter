using System;

namespace Kama.SmsService.Core.Model
{
    public class QueueItem
    {
        public Guid SourceAccountID { get; set; }

        public Core.Model.MessageReceiver MessageReceiver { get; set; }
    }
}
