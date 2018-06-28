using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Reflection;

namespace Kama.SmsService.Api.Controllers
{
    [RoutePrefix("api/v1/Send")]
    public class SendController : BaseApiController
    {
        public SendController(Core.Service.ISendService sendService)
        {
            _sendService = sendService;
        }

        readonly Core.Service.ISendService _sendService;

        [HttpPost, Route("Pause")]
        public AppCore.Result Pause()
            => _sendService.PauseProcess();

        [HttpPost, Route("Resume")]
        public AppCore.Result Resume()
            => _sendService.ResumeProcess();
    }
}
