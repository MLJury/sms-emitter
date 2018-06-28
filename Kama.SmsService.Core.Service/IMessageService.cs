using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kama.SmsService.Core.Service
{
	public interface IMessageService : IService
	{
        Task<AppCore.Result<Core.Model.Message>> SendAsync(Model.Message msg);

        Task<AppCore.Result> SendAsync(Model.Message[] model);

        //AppCore.Result ResumeProcess();

        //AppCore.Result PauseProcess();

        //Task<AppCore.Result> ProcessAsync();

        //Task<AppCore.Result> DoOnMainTimer();
    }
}
