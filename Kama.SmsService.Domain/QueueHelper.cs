using @CoreMode = Kama.SmsService.Core.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Kama.SmsService.Core.Service;

namespace Kama.SmsService.Domain
{
    class QueueHelper
    {

        readonly static Lazy<QueueHelper> _instance = new Lazy<QueueHelper>(() => new QueueHelper());

        public QueueHelper()
        {
            SendCountDictionary = new ConcurrentDictionary<CoreMode.SmsServiceAccounts, double>();
            foreach(var item in Enum.GetValues(typeof(CoreMode.SmsServiceAccounts)).Cast<CoreMode.SmsServiceAccounts>())
                SendCountDictionary.TryAdd(item, 0);
        }
        public static QueueHelper Instance
            => _instance.Value;

        Core.Model.PrioritySendCount config = null;
        Core.Model.Priority priority = Core.Model.Priority.VeryHigh;
        ushort counter = 0;

        public bool Running { get; set; }
        public ConcurrentDictionary<CoreMode.SmsServiceAccounts, double> SendCountDictionary { get; private set; }

        public void Load(Core.Model.PrioritySendCount config)
        {
            this.config = config;
            this.Running = true;
        }

        public Library.Queue.Priority CurrentPriority
              => (Library.Queue.Priority)((byte)priority);

        public void Next(IQueueService queue)
        {
            if (queue.QueueCount() > 0)
            {
                Next();
                while (queue.QueueCount((Library.Queue.Priority)priority) == 0)
                    Next();
            }
            else
                Running = false;
        }

        private void Next()
        {
            counter++;

            if (priority == Core.Model.Priority.VeryHigh && counter > config.VeryHigh)
            {
                counter = 0;
                priority = Core.Model.Priority.High;
            }
            else if (priority == Core.Model.Priority.High && counter > config.High)
            {
                counter = 0;
                priority = Core.Model.Priority.Medium;
            }
            else if (priority == Core.Model.Priority.Medium && counter > config.Medium)
            {
                counter = 0;
                priority = Core.Model.Priority.Normal;
            }
            else if (priority == Core.Model.Priority.Normal && counter > config.Normal)
            {
                counter = 0;
                priority = Core.Model.Priority.VeryHigh;
            }
        }
    }
}
