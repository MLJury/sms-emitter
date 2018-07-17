using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = SmsService.Core.DataSource;
using svc = SmsService.Core.Service;
using @Model = SmsService.Core.Model;
using Config = System.Configuration.ConfigurationManager;

namespace SmsService.Domain
{
    class SendTryService : Service, svc.ISendTryService
    {
        public SendTryService(AppCore.IOC.IContainer container)
            : base(container)
        {
            //_messageDataSource = messageDataSource;
        }

        //readonly ds.IMessageDataSource _messageDataSource;

        //public Task<AppCore.Result<Core.Model.Message>> SendAsync(Model.Message msg)
        //{
        //    msg.ID = Guid.NewGuid();
        //    msg.Priority = msg.Priority == Model.Priority.Unknown ? Model.Priority.Medium : msg.Priority;
        //    return _messageDataSource.CreateAsync(msg);
        //}

        //public Task<AppCore.Result> SendAsync(Model.Message[] messages)
        //{
        //    messages.ToList().ForEach(f => f.ID = Guid.NewGuid());
        //    messages.Where(w => w.Priority == Model.Priority.Unknown).ToList().ForEach(f => f.Priority = Model.Priority.Medium);
        //    return _messageDataSource.CreateAsync(messages);
        //}
    }
}
