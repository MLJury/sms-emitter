using System;
using System.Collections.Generic;

namespace Kama.SmsService.Infrastructure.MagfaSms
{
    class ErrorCode
    {
        readonly static Lazy<ErrorCode> _instance = new Lazy<ErrorCode>(() => new ErrorCode());

        readonly IDictionary<long, string> _items = new Dictionary<long, string> {
            { -1, "The target of report is not available(e.g. no message is associated with entered IDs)."},
            { 1, "The strings you presented as recipient numbers are not valid phone numbers, please check'em again."},
            { 2, "The strings you presented as sender numbers(3000-blah blah blahs) are not valid numbers, please check'em again."},
            { 3, "Are you sure you've entered the right encoding for this message? you can try other encodings to bypass this error code."},
            { 4, "Entered message class is not valid. for a normal MClass, leave this entry empty."},
            { 6, "Entered UDH is invalid. in order to send a simple message, leave this entry empty."},
            { 12, "You're trying to use a service from another account??? check your UN/Password/NumberRange again."},
            { 13, "Check the text of your message. it seems to be null."},
            { 14, "Your credit's not enough to send this message. you might want to buy some credit.call"},
            { 15, "Something bad happened on server side, you might want to call MAGFA Support about this:"},
            { 16, "Your account is not active right now, call -- to activate it."},
            { 17, "Looks like Your account's reached its expiration time, call -- for more information."},
            { 18, "The combination of entered Username/Password/Domain is not valid. check'em again."},
            { 19, "You're not entering the correct combination of Username/Password."},
            { 20, "Check the service type you're requesting. We don't get what service you want to use. Your sender number might be wrong, too."},
            { 22, "Your current number range doesn't have the permission to use Webservices."},
            { 23, "Sorry, Server's under heavy traffic pressure, try testing another time please."},
            { 24, "Entered message-id seems to be invalid, are you sure you entered the right thing?"},
            { 106, "Array of recipient numbers must have at least one member."},
            { 107, "The maximum number of recipients per message is 90."},
            { 108, "Array of sender numbers must have at least one member."},
            { 103, "This error happens when you have more than one sender-number for message. When you have more than one sender number, for each sender-number you must define a recipient number..."},
            { 101, "When you have N > 1 texts to send, you have to define N recipient-numbers..."},
            { 104, "This happens when you try to define UDHs for your messages. In this case you must define one recipient number for each udh."},
            { 102, "This happens when you try to define MClasses for your messages. In this case you must define one recipient number for each MClass."},
            { 109, "This happens when you try to define encodings for your messages. In this case you must define one recipient number for each Encoding."},
            { 110, "This happens when you try to define checking-message-ids for your messages. In this case you must define one recipient number for each checking-message-id."}
        };

        public static ErrorCode Instance
            => _instance.Value;

        public const int MaxValue = 1000;

        public string this[long code]
        {
            get
            {
                string msg = string.Empty;
                _items.TryGetValue(code, out msg);
                return msg;
            }
        }
    }
}
