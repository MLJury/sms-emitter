using System;

namespace SmsService.Core
{
    public interface IRequestInfo
    {
        Guid UserId { get; }

        Guid PositionId { get; }

        string Username { get; }

        string RemoteIP { get; }
    }
}
