namespace Kama.SmsService.Core.Model
{
    using System;
    using System.Collections.Generic;
    public class SmsConfig
    {
        public bool UseProxy { get; set; }

        public string ProxyAddress { get; set; }

        public string ProxyUsername { get; set; }

        public string ProxyPassword { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public string SenderNumber { get; set; }
    }
}
