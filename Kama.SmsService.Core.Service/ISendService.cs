using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace Kama.SmsService.Core.Service
{
    public interface ISendService : IService
    {
        AppCore.Result ResumeProcess();

        AppCore.Result PauseProcess();

        Task<AppCore.Result> ProcessAsync();

        void DoOnMainTimer(object sender, ElapsedEventArgs e);

        Task<AppCore.Result> LoadAsync();
    }
}
