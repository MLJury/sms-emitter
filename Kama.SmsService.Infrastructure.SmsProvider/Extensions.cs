using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Infrastructure.MagfaSms
{
    static class Extensions
    {
        public static long ToLong(this string s)
        {
            long result = -1;
            long.TryParse(s.Trim(), out result);
            return result;
        }
    }
}
