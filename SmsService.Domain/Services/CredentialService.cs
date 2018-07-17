using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ds = SmsService.Core.DataSource;
using svc = SmsService.Core.Service;
using @Model = SmsService.Core.Model;
using SmsService.Core.Model;
using System.Linq;

namespace SmsService.Domain
{
    class CredentialService : Service, svc.ICredentialService
    {
        public CredentialService(AppCore.IOC.IContainer container
                                , Core.Service.IMessageService messageService
                                , Core.SmsProvider.IOutgoingService outgoingService
                                , Core.DataSource.IAccountDataSource accountDataSource)
            : base(container)
        {
            _outgoingService = outgoingService;
            _accountDataSource = accountDataSource;
            _messageService = messageService;
        }
        readonly Core.Service.IMessageService _messageService;
        readonly Core.DataSource.IAccountDataSource _accountDataSource;
        readonly Core.SmsProvider.IOutgoingService _outgoingService;

        private Task<AppCore.Result<decimal>> _GetCreditValue(Model.Account account, Model.MessageReceiver recMessage)
            => _outgoingService.GetCredit(account);



        public async Task<AppCore.Result> CheckCredit(Model.Account account, Model.MessageReceiver recMessage)
        {
            var sendCount = QueueHelper.Instance.SendCountDictionary[recMessage.SourceAccount];
            if (sendCount % account.CreditAlertCount == 0)
            {
                var creditValue = await _GetCreditValue(account, recMessage);
                if (!creditValue.Success)
                    return creditValue;

                if (creditValue.Data <= account.AlertCreditAmount)
                {
                    var sendAlertResult = await _SendAlertAsync(account, recMessage, creditValue.Data);
                    if (!sendAlertResult.Success)
                        return sendAlertResult;
                }
            }

            QueueHelper.Instance.SendCountDictionary[recMessage.SourceAccount] = ++sendCount;
            return AppCore.Result.Successful();
        }

        private async Task<AppCore.Result> _SendAlertAsync(Model.Account account, Model.MessageReceiver recMessage, decimal remainingCredit)
        {
            var sendResult = await _messageService.SendAsync(new Message()
            {
                Content = $"اعتبار اکانت سامانه اس ام اس در حال اتمام است لطفا در اسرع وقت نسبت به شارژ آن اقدام نمایید"
                               + $"{remainingCredit} : باقی مانده شارژ حساب اس ام اسی"
                ,
                Priority = Priority.VeryHigh
                ,
                SourceAccount = recMessage.SourceAccount
                ,
                ReceiverNumbers = account.AdminNumbers.Select(s => s.Number).ToList()
            });

            return AppCore.Result.Successful();
        }
    }
}
