using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyWS
{
    class ErrorCodes
    {
        public static String getDescriptionForCode(int code)
        {
            switch (code)
            {
                case -1:
                    return "The target of report is not available(e.g. no message is associated with entered IDs)";
                case 1:
                    return "the Strings You presented as recipient numbers are not valid phone numbers, please check'em again";
                case 2:
                    return "the Strings You presented as sender numbers(3000-blah blah blahs) are not valid numbers, please check'em again";
                case 3:
                    return "are You sure You've entered the right encoding for this message? You can try other encodings to bypass this error code";
                case 4:
                    return "entered MessageClass is not valid. for a normal MClass, leave this entry empty";
                case 6:
                    return "entered UDH is invalid. in order to send a simple message, leave this entry empty";
                case 12:
                    return "you're trying to use a service from another account??? check your UN/Password/NumberRange again";
                case 13:
                    return "check the text of your message. it seems to be null.";
                case 14:
                    return "Your credit's not enough to send this message. you might want to buy some credit.call ";
                case 15:
                    return "something bad happened on server side, you might want to call MAGFA Support about this:";
                case 16:
                    return "Your account is not active right now, call -- to activate it";
                case 17:
                    return "looks like Your account's reached its expiration time, call -- for more information";
                case 18:
                    return "the combination of entered Username/Password/Domain is not valid. check'em again";
                case 19:
                    return "You're not entering the correct combination of Username/Password";
                case 20:
                    return "check the service type you're requesting. we don't get what service you want to use. your sender number might be wrong, too.";
                case 22:
                    return "your current number range doesn't have the permission to use Webservices";
                case 23:
                    return "Sorry, Server's under heavy traffic pressure, try testing another time please";
                case 24:
                    return "entered message-id seems to be invalid, are you sure You entered the right thing?";
                case 106:
                    return "array of recipient numbers must have at least one member";
                case 107:
                    return "the maximum number of recipients per message is 90";
                case 108:
                    return "array of sender numbers must have at least one member";
                case 103:
                    return "This error happens when you have more than one " +
                            "sender-number for message. when you have more than one sender number, for each sender-number you must " +
                            "define a recipient number...";
                case 101:
                    return "when you have N > 1 texts to send, you have to define N recipient-numbers...";
                case 104:
                    return "this happens when you try to define UDHs for your messages. in this case you must define one recipient number for each udh";
                case 102:
                    return "this happens when you try to define MClasses for your messages. in this case you must define one recipient number for each MClass";
                case 109:
                    return "this happens when you try to define encodings for your messages. in this case you must define one recipient number for each Encoding";
                case 110:
                    return "this happens when you try to define checking-message-ids for your messages. in this case you must define one recipient number for each checking-message-id";
                default:
                    return "";
            }
        }

        public static int MAX_VALUE = 1000;

    }
}
