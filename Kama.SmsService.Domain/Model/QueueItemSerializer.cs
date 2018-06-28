using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Domain
{
    class QueueItemSerializer : Kama.Library.Queue.ISerializer
    {
        public QueueItemSerializer(AppCore.IObjectSerializer objectSerializer)
        {
            _objectSerializer = objectSerializer;
        }

        readonly AppCore.IObjectSerializer _objectSerializer;

        public T Deserialize<T>(string data)
            => _objectSerializer.Deserialize<T>(data);

        public string Serialize<T>(T data)
            => _objectSerializer.Serialize(data);
    }
}
