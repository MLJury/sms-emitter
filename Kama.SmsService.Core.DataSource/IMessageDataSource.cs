using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kama.SmsService.Core.DataSource
{
	public interface IMessageDataSource : IDataSource
	{
        Task<AppCore.Result<Model.Message>> CreateAsync(Model.Message model);

        Task<AppCore.Result> CreateAsync(Model.Message[] model);

        Task<AppCore.Result> DeleteAsync(IEnumerable<Guid> ids);

        Task<AppCore.Result<IEnumerable<Model.Message>>> ListAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null);

        Task<AppCore.Result<IEnumerable<Model.Message>>> ListUnSentAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null);

        Task<AppCore.Result<IEnumerable<Model.MessageReceiver>>> ListUnQueueReceiverAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null);

        Task<AppCore.Result> SetQueueAsync(Guid receiverId, bool isQueue);

        Task<AppCore.Result> SetQueueAsync(List<Model.MessageReceiver> receivers, bool isQueue);

        Task<AppCore.Result> SetSendAsync(Core.Model.Send send);
    }
}
