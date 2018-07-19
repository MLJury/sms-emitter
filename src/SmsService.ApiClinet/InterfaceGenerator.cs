// App: SmsService.Client
// Developer: Payam Kandi
// Version: 1.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model = SmsService.Core.Model;


namespace SmsService.ApiClient.Interface
{
	public interface ISmsServiceClient: Library.ApiClient.IClient
	{
		string Host{get;}
	}

	public interface ISmsServiceHostInfo: Library.ApiClient.IHostInfo
	{
	}

	/// <summary>
    /// flag for ioc container registrar
    /// </summary>
	public interface IService
	{
	}

	public interface IMessageService: IService
	{
		
		Task<AppCore.Result<model.Message>> Send(model.Message msg,IDictionary<string, string> httpHeaders = null);
		
		Task<AppCore.Result> Send(model.Message[] msg,IDictionary<string, string> httpHeaders = null);
	}
	public interface ISendService: IService
	{
		
		Task<AppCore.Result> Pause(IDictionary<string, string> httpHeaders = null);
		
		Task<AppCore.Result> Resume(IDictionary<string, string> httpHeaders = null);
	}
	public interface ISendTryService: IService
	{
	}
}