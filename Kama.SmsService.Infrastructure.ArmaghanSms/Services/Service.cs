using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using @Model = Kama.SmsService.Core.Model;

namespace Kama.SmsService.Infrastructure.MagfaSms
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
