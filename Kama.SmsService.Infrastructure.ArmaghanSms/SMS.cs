using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;
using MagfaHttpServiceSamples;

namespace ProxyWS
{
    public class SMS
    {
        private static readonly String END_POINT_URL = "http://bulk.armaghan.net/post/sendSMS.ashx?";
        public SMS()
        {
        }

        public long enqueue(string userName, string password, string from, string to, string Text)
        {
            RequestHandler requestHandler = new RequestHandler();
            //making the url string
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("Username=").Append(userName).Append("&");
            sb.Append("Password=").Append(password).Append("&");
            sb.Append("from=").Append(from).Append("&");
            sb.Append("to=").Append(to).Append("&");
            sb.Append("Text=").Append(Text);
            String urlString = sb.ToString();

            String result = requestHandler.get(urlString);
            return long.Parse(result);
        }

        public async Task<long> enqueueAsync(string userName, string password, string from, string to, string Text)
        {
            RequestHandler requestHandler = new RequestHandler();
            //making the url string
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("Username=").Append(userName).Append("&");
            sb.Append("Password=").Append(password).Append("&");
            sb.Append("from=").Append(from).Append("&");
            sb.Append("to=").Append(to).Append("&");
            sb.Append("Text=").Append(Text);
            String urlString = sb.ToString();
            //var result = await Task.Run(() => requestHandler.get(urlString));
            var result = requestHandler.get(urlString);
            return long.Parse(result);
        }

        public long GetCredit(string userName, string password, string domain)
        {
            RequestHandler requestHandler = new RequestHandler();
            //making the url string
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=getCredit&");
            sb.Append("username=").Append(userName).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("domain=").Append(domain);
            String urlString = sb.ToString();
            //var result = await Task.Run(() => requestHandler.get(urlString));
            var result = requestHandler.get(urlString);
            return long.Parse(result);
        }

        public async Task<long> GetCreditAsync(string userName, string password, string domain)
        {
            RequestHandler requestHandler = new RequestHandler();
            //making the url string
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=getCredit&");
            sb.Append("username=").Append(userName).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("domain=").Append(domain);
            String urlString = sb.ToString();
            //var result = await Task.Run(() => requestHandler.get(urlString));
            var result = requestHandler.get(urlString);
            return long.Parse(result);
        }
    }
}
