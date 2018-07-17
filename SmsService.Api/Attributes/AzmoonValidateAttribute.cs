using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsService.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AzmoonValidateAttribute : Attribute
    {
    }
}