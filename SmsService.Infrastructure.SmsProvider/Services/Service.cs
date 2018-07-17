using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using @Model = SmsService.Core.Model;

namespace SmsService.Infrastructure.SmsProvider
{
    internal abstract class Service
    {
        public Service()
        {
            _sms = new ProxyWS.SMS();
        }

        protected readonly ProxyWS.SMS _sms;
    }
}
