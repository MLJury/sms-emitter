using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Net;
using Config = System.Configuration.ConfigurationManager;
using AppCore;
using System.Web.Http.Filters;
using System.Net.Http.Headers;

namespace API.Exceptions.Filters
{
    //A global exception handler that will be used to catch any error
    public class KamaExceptionHandler : IExceptionFilter
    {
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            string exceptionMessage = $"SmsService Exception: {GenerateExceptionMessageText.Instance[actionExecutedContext.Exception, Config.AppSettings["DeploymentMode"]]}";
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<AppCore.Result>(AppCore.Result.Failure(message: exceptionMessage)
                                                                                        , new System.Net.Http.Formatting.JsonMediaTypeFormatter()
                                                                                        , new MediaTypeHeaderValue("application/json"))
            };
            return Task.FromResult(0);
        }
        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}