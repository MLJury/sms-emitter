namespace Kama.SmsService.Tools
{
    class Logger: AppCore.EventLogger.WindowsEventLogger, Core.IEventLogger
    {
        public Logger()
            : base("Kama.SmsService")
        {
        }
    }
}
