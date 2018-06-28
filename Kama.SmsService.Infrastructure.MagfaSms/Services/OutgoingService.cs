using ProxyWS;
using System.Threading.Tasks;
using System.Web;
using @Model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.Infrastructure.MagfaSms
{
    class OutgoingService : Service, Core.MagfaSms.IOutgoingService
    {
        public async Task<AppCore.Result<long>> SendAsync(Model.MessageReceiver recMsg, Model.Account account)
        {
            var result = (await _sms.enqueueAsync(
                            userName: account.UserName
                            , password: account.Password
                            , domain: account.Domain
                            , useProxy: false
                            , proxyAddress: string.Empty
                            , proxyUsername: string.Empty
                            , proxyPassword: string.Empty
                            , count: 1
                            , senderNumber: account.Number
                            , recipientNumber: recMsg.ReceiverNumber
                            , text: recMsg.Content
                            ))[0];

            string finalResult;
            if (result < ErrorCodes.MAX_VALUE)
            {
                finalResult = "Error code: " + result + ", " + ErrorCodes.getDescriptionForCode((int)result);
                return AppCore.Result<long>.Failure(code: (int)result, message: finalResult, data: result);
            }
            else
                finalResult = result.ToString();

            return AppCore.Result<long>.Set(success: true, code: (int)result, data: result, message: finalResult);
        }

        public async Task<AppCore.Result<long>> GetMessageStatus(Model.Message msg, Model.Account account)
        {
            int statusResults = (await _sms.getMessageStatusesAsync(
                                    userName: account.UserName
                                    , password: account.Password
                                    , domain: account.Domain
                                    , useProxy: false
                                    , proxyAddress: string.Empty
                                    , proxyUsername: string.Empty
                                    , proxyPassword: string.Empty
                                    , messageIds: new long[] { msg.CheckId }))[0];

            string statusResultFinal;
            if (statusResults <= -1)
                statusResultFinal = "Error. code: " + statusResults + ", " + ErrorCodes.getDescriptionForCode(statusResults);
            else
                statusResultFinal = "Results: " + statusResults;

            return AppCore.Result<long>.Set(true, data: statusResults, message: statusResultFinal);
        }

        public async Task<AppCore.Result<decimal>> GetCredit(Model.Account account)
        {
            System.Single statusResults = (await _sms.getCreditAsync(
                                    userName: account.UserName
                                    , password: account.Password
                                    , domain: account.Domain
                                    , useProxy: false
                                    , proxyAddress: string.Empty
                                    , proxyUsername: string.Empty
                                    , proxyPassword: string.Empty));

            return AppCore.Result<decimal>.Set(true, data: (decimal)statusResults);
        }
    }
}
