using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = SmsService.Core.DataSource;
using @Model = SmsService.Core.Model;

namespace SmsService.Infrastructure.DAL
{
    class SendTryDataSource : DataSource, ds.ISendTryDataSource
    {
        public SendTryDataSource(AppCore.IOC.IContainer container)
            : base(container)
        {
        }

        public async Task<AppCore.Result> CreateTrySendAsync(Core.Model.SendTry sendTry)
        {
            try
            {
                return (await _dbMessage.AddTrySendMessageAsync(
                    _messageReceiverID: sendTry.ReceiverMessageID
                    , _message: sendTry.Message))
                    .ToActionResult<AppCore.Result>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
