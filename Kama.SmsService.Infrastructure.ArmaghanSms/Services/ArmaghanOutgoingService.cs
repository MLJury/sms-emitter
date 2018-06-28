using ProxyWS;
using System.Threading.Tasks;
using System.Web;
using @Model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.Infrastructure.MagfaSms
{
    class ArmaghanOutgoingService : Service, Core.MagfaSms.IArmaghanOutgoingService
    {
        public async Task<AppCore.Result<long>> SendAsync(Model.MessageReceiver msg, Model.Account account)
        {
            var result = (await _sms.enqueueAsync(
                    userName: account.UserName
                    , password: account.Password
                    , from: account.Number
                    , to: msg.ReceiverNumber
                    , Text: msg.Content));
            //(result == 1 ? true : false);
            return AppCore.Result<long>.Set(success: true, code: (int)result, data: result, message: null);
        }

        public async Task<AppCore.Result<decimal>> GetCreditAsync(Model.Account account)
        {
            var result = (await _sms.GetCreditAsync(
                    userName: account.UserName
                    , password: account.Password
                    , domain: account.Domain));
            //(result == 1 ? true : false);
            return AppCore.Result<decimal>.Set(success: true, code: (int)result, data: (decimal)result, message: null);
        }
    }
}
