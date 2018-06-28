using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kama.SmsService.Core.Service
{
    public interface ICredentialService : IService
    {
        Task<AppCore.Result> CheckCredit(Model.Account account, Model.MessageReceiver recMessage);
    }
}
