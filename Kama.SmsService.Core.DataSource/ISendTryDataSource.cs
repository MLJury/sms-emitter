using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kama.SmsService.Core.DataSource
{
    public interface ISendTryDataSource : IDataSource
    {
        Task<AppCore.Result> CreateTrySendAsync(Core.Model.SendTry sendTry);
    }
}
