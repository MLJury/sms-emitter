using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using svc = SmsService.Core.Service;
using @Model = SmsService.Core.Model;
using Config = System.Configuration.ConfigurationManager;
using System.Threading;
using System.Timers;
using SmsService.Core.Model;
using System.IO;
using AppCore.EventLogger;

namespace SmsService.Domain
{
    /// <summary>
    /// Core service for sending queued messages 
    /// </summary>
    class SendService : Service, svc.ISendService
    {
        public SendService(AppCore.IOC.IContainer container
                            , Core.IEventLogger logger
                           , Core.DataSource.IAccountDataSource accountDataSource
                           , Core.DataSource.IConfigDataSource configDataSource
                           , Core.DataSource.IMessageDataSource messageDataSource
                           , Core.DataSource.ISendTryDataSource sendTryDataSource
                           , Core.SmsProvider.IOutgoingService outgoingService
                           , Core.Service.ICredentialService credentialService
                           , Core.Service.IQueueService queueService)
            : base(container)
        {
            _logger = logger;
            _accountDataSource = accountDataSource;
            _configDataSource = configDataSource;
            _messageDataSource = messageDataSource;
            _outgoingService = outgoingService;
            _sendTryDataSource = sendTryDataSource;
            _credentialService = credentialService;
            _queueService = queueService;
        }
        readonly Core.IEventLogger _logger;
        readonly Core.Service.IQueueService _queueService;
        readonly Core.DataSource.IAccountDataSource _accountDataSource;
        readonly Core.DataSource.IConfigDataSource _configDataSource;
        readonly Core.DataSource.IMessageDataSource _messageDataSource;
        readonly Core.SmsProvider.IOutgoingService _outgoingService;
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
            sendResult = await _outgoingService.SendAsync(recMsg, account.Data);

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


        /// <summary>
        /// Loops till message queue become empty then stop looping
        /// </summary>
        /// <returns></returns>
        public async Task<AppCore.Result> ProcessAsync()
        {
            /*check if current priority is empty or not if is empty
             * then swich to other priorities, 
             * if all priorities all empty then set Running to false
            */
            if (_queueService.QueueCount(QueueHelper.Instance.CurrentPriority).Equals(0))
                QueueHelper.Instance.Next(_queueService);

            /*
             * Loop While queue message become empty
             */
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

        /*
         * Time works with specific interval values wich is 5 seconds by default  
         * and get all unqueued messages from database each time and enqueue them in 
         * Message Queue
         * interval value can be setted up in app.config => AutomationTimerInterval
         */
        public void DoOnMainTimer(object sender, ElapsedEventArgs e)
        {
            var unQueueRecList = _messageDataSource.ListUnQueueReceiverAsync(Guid.Empty).GetAwaiter().GetResult();

            if (unQueueRecList.Data.Any())
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
