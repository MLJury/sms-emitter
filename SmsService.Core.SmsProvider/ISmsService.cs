using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsService.Core.SmsProvider
{
    public interface ISmsService : IMagfaService
    {
        Task<AppCore.Result<Model.Sms>> SendMessageAsync(Model.Sms sms, Model.SmsConfig config);

        Task<AppCore.Result<Model.Sms>> CreditAsync(Model.SmsConfig config);

        Task<AppCore.Result<Model.Sms>> GetMessageByIDAsync(Model.Sms sms, Model.SmsConfig config);

        Task<AppCore.Result<Model.Sms>> GetMessageStatusAsync(Model.Sms sms, Model.SmsConfig config);

        Task<AppCore.Result<IEnumerable<Model.Sms>>> GetAllMessageAsync(Model.Sms sms, Model.SmsConfig config);

        Task<AppCore.Result<IEnumerable<Model.Sms>>> GetAllMessagesByNumberAsync(Model.Sms sms, Model.SmsConfig config);
    }
}
