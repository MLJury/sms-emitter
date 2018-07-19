using SmsService.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Core.Service
{
    public interface IQueueService : IService
    {
        AppCore.Result Enqueue(IEnumerable<MessageReceiver> recMsg);

        AppCore.Result Enqueue(MessageReceiver recMsg);

        AppCore.Result Enqueue(Library.Queue.Packet<QueueItem> queuePacket);
        Library.Queue.ITransaction<QueueItem> Dequeue(Library.Queue.Priority priority, TimeSpan timeSpan);

        int QueueCount(Library.Queue.Priority priority);

        int QueueCount();

    }
}
