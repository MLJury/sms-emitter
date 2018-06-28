using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = Kama.SmsService.Core.DataSource;
using @Model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.Infrastructure.DAL
{
	class MessageDataSource : DataSource, ds.IMessageDataSource
	{
		public MessageDataSource(AppCore.IOC.IContainer container)
			: base(container)
		{
		}

        public async Task<AppCore.Result<Model.Message>> CreateAsync(Model.Message model)
        {
            try
            {
                var result = (await _dbMessage.AddMessageAsync(
                    _id: model.ID,
                    _sourceAccountID: Model.SmsServiceDic.Instance[model.SourceAccount],
                    _priority: (byte)model.Priority,
                    _sendType: (byte)model.SendType,
                    _encodingType: (byte)model.Encoding,
                    _status: (short)model.Status,
                    _sendDate: DateTime.Now,
                    _externalMessageID: model.ExternalMessageID,
                    _uDH: model.UDH,
                    _content: model.Content,
                    _receiverNumbers: Newtonsoft.Json.JsonConvert.SerializeObject(model.ReceiverNumbers)
                    )).ToActionResult<Model.Message>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> CreateAsync(Model.Message[] model)
        {
            try
            {
                var result = (await _dbMessage.AddMessagesAsync(
                        _messages: Newtonsoft.Json.JsonConvert.SerializeObject(model)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> DeleteAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var result = (await _dbMessage.DeleteOutgoingMessageAsync(_iDS: $"[{string.Join(",", ids.ToArray())}]"))
                        .ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.Message>>> ListAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null)
        {
            try
            {
                short? xStatus = null;
                if (status != null && status.HasValue)
                    xStatus = (short)status;

                var result = (await _dbMessage.GetMessagesAsync(_sourceAccountID: sourceAccountID, _status: xStatus, _receiverNumber: string.IsNullOrWhiteSpace(receiverNumber) ? null : receiverNumber, _sendDateFrom: sendDateFrom, _sendDateTo: sendDateTo))
                        .ToListActionResult<Model.Message>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.Message>>> ListUnSentAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null)
        {
            try
            {
                var result = (await _dbMessage.GetUnQueueMessagesAsync())
                        .ToListActionResult<Model.Message>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.MessageReceiver>>> ListUnQueueReceiverAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null)
        {
            try
            {
                var result = (await _dbMessage.GetUnQueueReceiverMessagesAsync())
                        .ToListActionResult<Model.MessageReceiver>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SetQueueAsync(Guid Id, bool isQueue)
        {
            try
            {
                var result = (await _dbMessage.SetQueueMessageReceiverAsync(_id: Id, _isQueue: isQueue))
                        .ToListActionResult<AppCore.Result>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SetQueueAsync(List<Model.MessageReceiver> receivers, bool isQueue)
        {
            try
            {
                var result = (await _dbMessage.SetQueueMessageReceiversAsync(_iDs: Newtonsoft.Json.JsonConvert.SerializeObject(receivers.Select(s => s.ID)), _isQueue: isQueue))
                        .ToListActionResult<AppCore.Result>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SetSendAsync(Core.Model.Send send)
        {
            try
            {
                var result = (await _dbMessage.SetSendMessageReceiverAsync(
                    _id: send.ReceiverMessageID
                        , _isSent: send.IsSent
                        , _message: send.Message))
                        .ToListActionResult<AppCore.Result>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
