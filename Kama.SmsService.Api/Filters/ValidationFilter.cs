using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using Kama.SmsService.Core.Service;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Net;
using Kama.Mefa.Azmoon.API.Auth;
using Kama.SmsService.Api.Attributes;

namespace Kama.Mefa.Azmoon.API.Auth.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
                                                HttpActionContext actionContext,
                                                CancellationToken cancellationToken,
                                                Func<Task<HttpResponseMessage>> continuation)
        {
            var azmoonValidateAttribute = actionContext
                    .ControllerContext
                    .ControllerDescriptor
                    .GetCustomAttributes<AzmoonValidateAttribute>()
                    .SingleOrDefault();

            //check whether modelState is valid or not
            if (azmoonValidateAttribute == null || actionContext.ModelState.IsValid)
                return await continuation();

            //generate errors
            string errors = String.Join("&&", actionContext.ModelState.Values.Select(s => s.Errors.FirstOrDefault().ErrorMessage));

            return actionContext.Request.CreateResponse(AppCore.Result.Failure(-1, errors));
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}