using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MagfaHttpServiceSamples
{
    class UrlGenerator
    {
        private static readonly String END_POINT_URL = "http://sms.magfa.com/magfaHttpService?";

        public static String generateEnqueueUrl(String username, String password, String domain, String senderNumber, String recipientNumber, String text, String udh, String encoding, String checkingMessageId)
        {
            //making the url string
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=enqueue&");
            sb.Append("username=").Append(username).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("from=").Append(senderNumber).Append("&");
            sb.Append("to=").Append(recipientNumber).Append("&");
            sb.Append("domain=").Append(domain).Append("&");
            sb.Append("message=").Append(HttpUtility.UrlEncode(text)).Append("&");
            sb.Append("udh=").Append(udh).Append("&");
            sb.Append("coding=").Append(encoding).Append("&");
            sb.Append("chkmessageid=").Append(checkingMessageId);
            return sb.ToString();
        }

        public static String generateGetCreditUrl(String username, String password, String domain)
        {
            //making the url string
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=getCredit&");
            sb.Append("username=").Append(username).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("domain=").Append(domain);
            return sb.ToString();
        }

        public static String generateGetMessageIdUrl(String username, String password, String domain, String checkingMessageId)
        {
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=getMessageId&");
            sb.Append("username=").Append(username).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("chkmessageid=").Append(checkingMessageId);
            return sb.ToString();
        }

        public static String generateGetMessageStatusUrl(String username, String password, String domain, String messageId)
        {
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=getMessageStatus&");
            sb.Append("username=").Append(username).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("messageId=").Append(messageId);
            return sb.ToString();
        }

        public static String generateGetRealMessageStatusUrl(String username, String password, String domain, String messageId)
        {
            StringBuilder sb = new StringBuilder(END_POINT_URL);
            sb.Append("service=getRealMessageStatus&");
            sb.Append("username=").Append(username).Append("&");
            sb.Append("password=").Append(password).Append("&");
            sb.Append("messageId=").Append(messageId);
            return sb.ToString();
        }
    }
}
