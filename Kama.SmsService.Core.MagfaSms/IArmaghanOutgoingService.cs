using System.Threading.Tasks;

namespace Kama.SmsService.Core.MagfaSms
{
    public interface IArmaghanOutgoingService : IMagfaService
    {
        //Model.Account account, Model.Message message
        Task<AppCore.Result<long>> SendAsync(Model.MessageReceiver recMsg, Model.Account account);
        Task<AppCore.Result<decimal>> GetCreditAsync(Model.Account account);
    }
}
