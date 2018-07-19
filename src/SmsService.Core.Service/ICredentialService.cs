using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Core.Service
{
    public interface ICredentialService : IService
    {
        Task<AppCore.Result> CheckCredit(Model.Account account, Model.MessageReceiver recMessage);
    }
}
