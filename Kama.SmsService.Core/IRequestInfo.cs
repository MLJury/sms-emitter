using System;

namespace Kama.SmsService.Core
{
    public interface IRequestInfo
    {
        Guid UserId { get; }

        Guid PositionId { get; }

        string Username { get; }

        string RemoteIP { get; }
    }
}
