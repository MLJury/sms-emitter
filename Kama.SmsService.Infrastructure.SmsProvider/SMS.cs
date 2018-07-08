using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;

namespace ProxyWS
{
    public class SMS
    {
        public SMS()
        {
        }

        public long[] enqueue(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, int count, String senderNumber, String recipientNumber, String text)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            long[] results;

            string[] messages;
            string[] mobiles;
            string[] origs;

            int[] encodings;
            string[] UDH;
            int[] mclass;
            int[] priorities;
            long[] checkingIds;

            messages = new string[count];
            mobiles = new string[count];
            origs = new string[count];

            encodings = new int[count];
            UDH = new string[count];
            mclass = new int[count];
            priorities = new int[count];
            checkingIds = new long[count];
            /*
            encodings = null;
            UDH = null;
            mclass = null;
            priorities = null;
            checkingIds = null;
            */
            for (int i = 0; i < count; i++)
            {
                messages[i] = text;
                mobiles[i] = recipientNumber;
                origs[i] = senderNumber;

                encodings[i] = -1;
                UDH[i] = "";
                mclass[i] = -1;
                priorities[i] = -1;
                checkingIds[i] = 200 + i;
            }

            return sq.enqueue(domain , messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds);
        }

        public async Task<long[]> enqueueAsync(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, int count, String senderNumber, String recipientNumber, String text)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            long[] results;

            string[] messages;
            string[] mobiles;
            string[] origs;

            int[] encodings;
            string[] UDH;
            int[] mclass;
            int[] priorities;
            long[] checkingIds;

            messages = new string[count];
            mobiles = new string[count];
            origs = new string[count];

            encodings = new int[count];
            UDH = new string[count];
            mclass = new int[count];
            priorities = new int[count];
            checkingIds = new long[count];
            /*
            encodings = null;
            UDH = null;
            mclass = null;
            priorities = null;
            checkingIds = null;
            */
            for (int i = 0; i < count; i++)
            {
                messages[i] = text;
                mobiles[i] = recipientNumber;
                origs[i] = senderNumber;

                encodings[i] = -1;
                UDH[i] = "";
                mclass[i] = -1;
                priorities[i] = -1;
                checkingIds[i] = 200 + i;
            }

            return await Task.Run(() => sq.enqueue(domain, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds));
        }

        public System.Single getCredit(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return sq.getCredit(domain);
        }

        public async Task<System.Single> getCreditAsync(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return await Task.Run(() =>  sq.getCredit(domain));
        }

        public long getMessageById(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, long checkingMessageId)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return sq.getMessageId(domain, checkingMessageId);
        }

        public async Task<long> getMessageByIdAsync(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, long checkingMessageId)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return await Task.Run(() => sq.getMessageId(domain, checkingMessageId));
        }

        public int[] getMessageStatuses(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, long[] messageIds)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return sq.getMessageStatuses(messageIds);
        }

        public async Task<int[]> getMessageStatusesAsync(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, long[] messageIds)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return await Task.Run(() => sq.getMessageStatuses(messageIds));
        }

        public MagfaWebReference.CustomerReturnIncomingFormat[] getAllMessages(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, int numberOfMessages)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return (MagfaWebReference.CustomerReturnIncomingFormat[])sq.getAllMessages(domain, numberOfMessages);
        }

        public async Task<MagfaWebReference.CustomerReturnIncomingFormat[]> getAllMessagesAsync(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, int numberOfMessages)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            return await Task.Run(() => (MagfaWebReference.CustomerReturnIncomingFormat[])sq.getAllMessages(domain, numberOfMessages));
        }

        public MagfaWebReference.CustomerReturnIncomingFormat[] getAllMessagesWithNumber(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, int numberOfMessages, String destNumber)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            System.Object[] shit = sq.getAllMessagesWithNumber(domain, numberOfMessages, destNumber);
            MagfaWebReference.CustomerReturnIncomingFormat[] custs = new MagfaWebReference.CustomerReturnIncomingFormat[shit.Length];
            for (int index = 0; index < shit.Length; index++)
            {
                custs[index] = (MagfaWebReference.CustomerReturnIncomingFormat)shit[index];
            }
            return custs;
        }

        public async Task<MagfaWebReference.CustomerReturnIncomingFormat[]> getAllMessagesWithNumberAsync(string userName, string password, string domain, bool useProxy, string proxyAddress, string proxyUsername, string proxyPassword, int numberOfMessages, String destNumber)
        {
            MagfaWebReference.SoapSmsQueuableImplementationService sq = new MagfaWebReference.SoapSmsQueuableImplementationService();
            if (useProxy)
            {
                WebProxy proxy;
                proxy = new WebProxy(proxyAddress);
                proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                sq.Proxy = proxy;
            }
            sq.Credentials = new System.Net.NetworkCredential(userName, password);
            sq.PreAuthenticate = true;
            System.Object[] shit = sq.getAllMessagesWithNumber(domain, numberOfMessages, destNumber);
            MagfaWebReference.CustomerReturnIncomingFormat[] custs = new MagfaWebReference.CustomerReturnIncomingFormat[shit.Length];
            for (int index = 0; index < shit.Length; index++)
            {
                custs[index] = (MagfaWebReference.CustomerReturnIncomingFormat)shit[index];
            }
            return await Task.Run(() => custs);
        }
    }
}
