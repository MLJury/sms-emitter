using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Infrastructure.MagfaSms
{
    internal abstract class DataSource
    {
        public DataSource()
        {
            _sms = new ProxyWS.SMS();
        }

        protected readonly ProxyWS.SMS _sms;
    }
}
