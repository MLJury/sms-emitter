using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using svc = Kama.SmsService.Core.Service;
using @Model = Kama.SmsService.Core.Model;
using Config = System.Configuration.ConfigurationManager;
using System.Threading;
using System.Timers;
using Kama.SmsService.Core.Model;
using System.IO;
using Kama.AppCore.EventLogger;

namespace Kama.SmsService.Domain
{
    class SendService : Service, svc.ISendService
    {
        public SendService(AppCore.IOC.IContainer container
                            , Core.IEventLogger logger
                           , Core.DataSource.IAccountDataSource accountDataSource
                           , Core.DataSource.IConfigDataSource configDataSource
                           , Core.DataSource.IMessageDataSource messageDataSource
                           , Core.DataSource.ISendTryDataSource sendTryDataSource
                           , Core.MagfaSms.IOutgoingService magfaOutgoingService
                           , Core.MagfaSms.IArmaghanOutgoingService armaghanOutgoingService
                           , Core.Service.ICredentialService credentialService
                           , Core.Service.IQueueService queueService)
            : base(container)
        {
            _logger = logger;
            _accountDataSource = accountDataSource;
            _configDataSource = configDataSource;
            _messageDataSource = messageDataSource;
            _magfaOutgoingService = magfaOutgoingService;
            _armaghanOutgoingService = armaghanOutgoingService;
            _sendTryDataSource = sendTryDataSource;
            _credentialService = credentialService;
            _queueService = queueService;
        }
        readonly Core.IEventLogger _logger;
        readonly Core.Service.IQueueService _queueService;
        readonly Core.DataSource.IAccountDataSource _accountDataSource;
        readonly Core.DataSource.IConfigDataSource _configDataSource;
        readonly Core.DataSource.IMessageDataSource _messageDataSource;
        readonly Core.MagfaSms.IOutgoingService _magfaOutgoingService;
        readonly Core.MagfaSms.IArmaghanOutgoingService _armaghanOutgoingService;
        readonly Core.Service.ICredentialService _credentialService;
        readonly Core.DataSource.ISendTryDataSource _sendTryDataSource;
        public AppCore.Result ResumeProcess()
        {
            QueueHelper.Instance.Running = true;
            return AppCore.Result.Successful();
        }

        public AppCore.Result PauseProcess()
        {
            QueueHelper.Instance.Running = false;
            return AppCore.Result.Successful();
        }

        private async Task _SendFailedAsync(Library.Queue.ITransaction<QueueItem> qResult, string message)
        {
            if (Int32.Parse(Config.AppSettings["MaximumSendTry"]) > qResult.Data.TryCount)
            {
                await _sendTryDataSource.CreateTrySendAsync(new SendTry
                {
                    ReceiverMessageID = qResult.Data.Id
                ,
                    Message = message
                });
                _queueService.Enqueue(qResult.Data);
            }
            else
                await _messageDataSource.SetSendAsync(new Send
                {
                    ReceiverMessageID = qResult.Data.Data.MessageReceiver.ID
                ,
                    IsSent = false
                ,
                    Message = message
                });
        }

        private async Task<AppCore.Result> _SendAsync(Model.MessageReceiver recMsg)
        {
            var account = await _accountDataSource.GetAsync(recMsg.SourceAccountID);
            if (!account.Success)
                return AppCore.Result.Failure(message: account.Message, code: account.Code);

            AppCore.Result<long> sendResult;
            if (recMsg.SourceAccount == SmsServiceAccounts.Aro)
                sendResult = await _armaghanOutgoingService.SendAsync(recMsg, account.Data);
            else
                sendResult = await _magfaOutgoingService.SendAsync(recMsg, account.Data);

            if (sendResult.Success)
            {
                var checkCredit = await _credentialService.CheckCredit(account.Data, recMsg);
                //if(!checkCredit.Success)
                //log goes here
            }
            return sendResult;
        }


        public async Task<AppCore.Result> LoadAsync()
        {
            var rsGetConfig = await _configDataSource.GetPrioritySendCountAsync();
            if (!rsGetConfig.Success)
                return AppCore.Result.Failure(message: rsGetConfig.Message);
            else if (rsGetConfig.Data == null)
                return AppCore.Result.Failure(message: "Service configuration not exist.");

            QueueHelper.Instance.Load(rsGetConfig.Data);

            return AppCore.Result.Successful();
        }

        public async Task<AppCore.Result> ProcessAsync()
        {
            if (_queueService.QueueCount(QueueHelper.Instance.CurrentPriority).Equals(0))
                QueueHelper.Instance.Next(_queueService);

            while (QueueHelper.Instance.Running)
            {
                Library.Queue.ITransaction<QueueItem> qResult = null;
                try
                {
                    qResult = _queueService.Dequeue(QueueHelper.Instance.CurrentPriority, TimeSpan.FromMilliseconds(300));

                    var sendResult = await _SendAsync(qResult.Data.Data.MessageReceiver);

                    if (!sendResult.Success)
                        await _SendFailedAsync(qResult, sendResult.Message);
                    else
                        await _messageDataSource.SetSendAsync(new Send
                        {
                            ReceiverMessageID = qResult.Data.Data.MessageReceiver.ID
                        ,
                            IsSent = true
                        ,
                            Message = "Message was Sent Successfully"
                        });
                }
                catch (Exception e)
                {
                    _logger?.Error(e);
                    await _SendFailedAsync(qResult, e.Message);
                }
                finally
                {
                    QueueHelper.Instance.Next(_queueService);
                }
            }

            return AppCore.Result.Successful();
        }

        public void DoOnMainTimer(object sender, ElapsedEventArgs e)
        {
            var unQueueRecList = _messageDataSource.ListUnQueueReceiverAsync(Guid.Empty).GetAwaiter().GetResult();

            if (unQueueRecList.Data.Count() > 0)
            {
                var setQueueResult = _messageDataSource.SetQueueAsync(unQueueRecList.Data.ToList(), true);
                if (setQueueResult.GetAwaiter().GetResult().Success)
                {
                    _queueService.Enqueue(unQueueRecList.Data);
                    QueueHelper.Instance.Running = true;
                    ProcessAsync().GetAwaiter().GetResult();
                }
            }
        }
    }
}
