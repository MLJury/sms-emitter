using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Reflection;

namespace SmsService.Api.Controllers
{
    [RoutePrefix("api/v1/Message")]
    public class MessageController : BaseApiController
    {
        public MessageController(Core.Service.IMessageService messageService)
        {
            _messageService = messageService;
        }

        readonly Core.Service.IMessageService _messageService;

        [HttpPost, Route("Send")]
        public Task<AppCore.Result<Core.Model.Message>> Send(Core.Model.Message msg)
            => _messageService.SendAsync(msg);

        [HttpPost, Route("Send/Bulk")]
        public Task<AppCore.Result> Send(Core.Model.Message[] msg)
            => _messageService.SendAsync(msg);
    }
}
