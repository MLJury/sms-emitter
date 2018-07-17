using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ds = SmsService.Core.DataSource;
using svc = SmsService.Core.Service;
using @Model = SmsService.Core.Model;
using SmsService.Core.Model;
using System.Linq;

namespace SmsService.Domain
{
    class QueueService : Service, svc.IQueueService
    {
        public QueueService(AppCore.IOC.IContainer container)
            : base(container)
        {
            _queue = new Library.Queue.Queue<QueueItem>("SmsService", new QueueItemSerializer(_objectSerializer));
        }
        readonly Library.Queue.Queue<QueueItem> _queue;

        private Library.Queue.Packet<QueueItem> ConvertToPacket(Core.Model.MessageReceiver recMsg)
              => new Library.Queue.Packet<QueueItem>
              {
                  Label = recMsg.ID.ToString("N"),
                  AppSpecific = 1,
                  CorrelationId = recMsg.ID.ToString("N"),
                  Id = recMsg.ID,
                  Priority = (Library.Queue.Priority)((byte)recMsg.Priority),
                  Data = new QueueItem { SourceAccountID = recMsg.SourceAccountID, MessageReceiver = recMsg },
                  TryCount = 0
              };

        public AppCore.Result Enqueue(IEnumerable<Core.Model.MessageReceiver> recMsg)
        {
            _queue.Enqueue(recMsg.Select(s => ConvertToPacket(s)));
            return AppCore.Result.Successful();
        }

        public AppCore.Result Enqueue(Core.Model.MessageReceiver recMsg)
        {
            _queue.Enqueue(ConvertToPacket(recMsg));
            return AppCore.Result.Successful();
        }

        public AppCore.Result Enqueue(Library.Queue.Packet<SmsService.Core.Model.QueueItem> queuePacket)
        {
            _queue.Enqueue(queuePacket);

            return AppCore.Result.Successful();
        }

        public Library.Queue.ITransaction<SmsService.Core.Model.QueueItem> Dequeue(Library.Queue.Priority priority, TimeSpan timeSpan)
        {
            var qResult = _queue.Dequeue(priority, timeSpan);
            qResult.Data.TryCount = qResult.Data.TryCount + 1;
            qResult?.Commit();
            return qResult;
        }

        public int QueueCount()
        {
            int veryHighCount = QueueCount(Library.Queue.Priority.VeryHeigh);
            int HighCount = QueueCount(Library.Queue.Priority.High);
            int MediumCount = QueueCount(Library.Queue.Priority.Medium);
            int NormalCount = QueueCount(Library.Queue.Priority.Normal);
            int queueCount = veryHighCount + HighCount + MediumCount + NormalCount;
            return queueCount;
        }
        //=> (QueueCount(Library.Queue.Priority.VeryHeigh)
        //    + QueueCount(Library.Queue.Priority.High)
        //    + QueueCount(Library.Queue.Priority.Medium)
        //    + QueueCount(Library.Queue.Priority.Normal));

        public int QueueCount(Library.Queue.Priority priority)
        {
            var queueItem = _queue[priority];
            int queueCount = queueItem.Count();
            return queueCount;
        }
    }
}
