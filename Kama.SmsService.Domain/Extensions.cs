using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Domain
{
    static class Extensions
    {
        public static bool IsNullOrEmpty(this Guid guid)
            => guid == null || guid.Equals(Guid.Empty);
    }
}
