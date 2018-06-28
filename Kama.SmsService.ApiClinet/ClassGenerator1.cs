// App: SmsService.Client
// Developer: Payam Kandi
// Version: 1.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kama.SmsService.ApiClient.Interface;
using model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.ApiClient
{
	/// <summary>
    /// flag for ioc container registrar
    /// </summary>
	abstract class Service
    {
    }

	partial class MessageService: Service, IMessageService
	{
		public MessageService(ISmsServiceClient client)
		{
			_client = client;
		}
		readonly ISmsServiceClient _client;

        
		public virtual Task<AppCore.Result<model.Message>> Send(model.Message msg,IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{{"msg",msg.ToString()}};
			const string url = "api/v1/Message/Send";
			return _client.SendAsync<model.Message>(true, url, routeParamValues, httpHeaders, msg);
		}
        
		public virtual Task<AppCore.Result> Send(model.Message[] msg,IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{{"msg",msg.ToString()}};
			const string url = "api/v1/Message/Send/Bulk";
			return _client.SendAsync(true, url, routeParamValues, httpHeaders, msg);
		}
	}

	partial class SendService: Service, ISendService
	{
		public SendService(ISmsServiceClient client)
		{
			_client = client;
		}
		readonly ISmsServiceClient _client;

        
		public virtual Task<AppCore.Result> Pause(IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{};
			const string url = "api/v1/Send/Pause";
			return _client.SendAsync(true, url, routeParamValues, httpHeaders);
		}
        
		public virtual Task<AppCore.Result> Resume(IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{};
			const string url = "api/v1/Send/Resume";
			return _client.SendAsync(true, url, routeParamValues, httpHeaders);
		}
	}

	partial class SendTryService: Service, ISendTryService
	{
		public SendTryService(ISmsServiceClient client)
		{
			_client = client;
		}
		readonly ISmsServiceClient _client;

	}
}