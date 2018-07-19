using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Core.DataSource
{
    public interface ISendTryDataSource : IDataSource
    {
        Task<AppCore.Result> CreateTrySendAsync(Core.Model.SendTry sendTry);
    }
}
