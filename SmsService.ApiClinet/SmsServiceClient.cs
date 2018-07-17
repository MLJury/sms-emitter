using System;
using System.Collections.Generic;

namespace SmsService.ApiClient
{
    class SmsServiceClient : Library.ApiClient.Client, Interface.ISmsServiceClient
    {
        public SmsServiceClient(AppCore.IObjectSerializer objectSerializer, string host)
            : base(objectSerializer, host)
        {
            _host = host;
        }

        public SmsServiceClient(AppCore.IObjectSerializer objectSerializer, string host, Func<IDictionary<string, string>> defaultHeaders)
            : base(objectSerializer, host, defaultHeaders)
        {
            _host = host;
        }

        readonly string _host;

        public string Host => _host;
    }
}
