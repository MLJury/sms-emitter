using System;
using System.Collections.Generic;
using System.Linq;

namespace SmsService.Infrastructure.DAL
{
    static class Extensions
    {
        public static bool DbSucceed(this DatabaseModel.ResultSet result)
            => result.ReturnValue >= 0;

        public static AppCore.Result ToActionResult(this DatabaseModel.ResultSet result, string message = "")
            => AppCore.Result.Set(success: result.ReturnValue >= 0, code: Math.Abs(result.ReturnValue), message: message);

        public static AppCore.Result<T> ToActionResult<T>(this DatabaseModel.ResultSet result, string message = "")
            where T : class, new()
            => AppCore.Result<T>.Set(success: result.ReturnValue >= 0, code: Math.Abs(result.ReturnValue), data: result.GetModels<T>().FirstOrDefault(), message: message);

        public static AppCore.Result<IEnumerable<T>> ToListActionResult<T>(this DatabaseModel.ResultSet result, string message = "")
            where T : class, new()
            => AppCore.Result<IEnumerable<T>>.Set(success: result.ReturnValue >= 0, code: Math.Abs(result.ReturnValue), data: result.GetModels<T>(), message: message);
    }
}
