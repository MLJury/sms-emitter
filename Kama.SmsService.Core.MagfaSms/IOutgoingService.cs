using System.Threading.Tasks;

namespace Kama.SmsService.Core.MagfaSms
{
    public interface IOutgoingService: IMagfaService
    {
        //Model.Account account, Model.Message message
        Task<AppCore.Result<long>> SendAsync(Model.MessageReceiver msg, Model.Account account);

        Task<AppCore.Result<long>> GetMessageStatus(Model.Message msg, Model.Account account);

        Task<AppCore.Result<decimal>> GetCredit(Model.Account account);
    }
}
