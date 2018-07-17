using System;
using System.Collections.Generic;

namespace SmsService.Core.Model
{
    public class Message: Model
    {
        /// <summary>
        /// شناسه پیامک برروی دیتابیس ارائه دهنده سرویس ارسال پیامک
        /// مثال: شناسه پیامک در دیتابیس مگفا
        /// </summary>
        public long ExternalMessageID { get; set; }

        public long CheckId
        {
            get
            {
                if (ID == null || ID == Guid.Empty)
                    return 0;
                else
                {
                    var hCode = ID.ToString("N").GetHashCode();
                    var s = $"{(hCode < 0 ? 1 : 2)}0{Math.Abs(hCode)}";
                    long id = 0;
                    long.TryParse(s, out id);
                    return id;
                }
            }
        }

        public SmsServiceAccounts SourceAccount { get; set; }

        public Guid SourceAccountID { get; set; }

        public string SourceAccountNumber { get; set; }

        public string SourceAccountTitle { get; set; }

        public SendType SendType { get; set; }

        public EncodingType Encoding { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string UDH { get; set; }

        public string Content { get; set; }

        public List<string> ReceiverNumbers { get; set; }
    }
}
