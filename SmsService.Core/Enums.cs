using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Core
{
    public enum Priority : byte
    {
        Normal = 1,
        Medium = 2,
        High = 4,
        VeryHigh = 8
    }

    public enum SendType:byte
    {
        /// <summary>
        /// پیامک بصورت مستقیم روی صفحه دستگاه گیرنده فرستاده می شود.
        /// </summary>
        OnDeviceScreen = 0,

        /// <summary>
        /// پیامک در حافظه دستگاه گیرنده ذخیره می گردد.
        /// </summary>
        SaveInDevice = 1,

        /// <summary>
        /// پیامک بر روی سیم کارت ذخیره می شود.
        /// </summary>
        SaveInSim = 2,

        /// <summary>
        /// در صورتیکه دستگاه گیرنده دارای یک نرم افزار خاص برای ذخیره پیامک باشد
        /// یا به یک نرم افزار کاربردی خاص روی یک کامپیوتر متصل باشد، پیامک در آن نرم افزار ذخیره می شود.
        /// </summary>
        SaveInCustomApp = 3
    }

    public enum EncodingType: byte
    {
        Auto = 0,
        UTF8 = 2,
        OctedStream = 5,
        Binary = 6
    }

    public enum Status: short
    {
        /// <summary>
        /// وضعیت نا مشخص
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// پیامک به گیرنده تحویل داده شده است
        /// </summary>
        Delivered = 1,

        /// <summary>
        /// پیامک به گیرنده نرسیده است
        /// </summary>
        NotDelivered = 2,

        /// <summary>
        /// پیامک به مخابرات رسیده است
        /// </summary>
        OperatorDelivered = 8,

        /// <summary>
        /// پیامک به مخابرات ترسیده است
        /// </summary>
        OperatorNotDelivered = 16,

        /// <summary>
        /// شناسه پیامک نامعتبر است
        /// </summary>
        UnknwonID = -1
    }
}
