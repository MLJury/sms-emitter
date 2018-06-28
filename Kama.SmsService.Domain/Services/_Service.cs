
namespace Kama.SmsService.Domain
{
    class Service
    {
        public Service(AppCore.IOC.IContainer container)
        {
            _container = container;
            _objectSerializer = container.TryResolve<AppCore.IObjectSerializer>();
        }

        protected readonly AppCore.IOC.IContainer _container;
        protected readonly AppCore.IObjectSerializer _objectSerializer;
    }

    class Service<TDataSource> : Service
        where TDataSource : Core.DataSource.IDataSource
    {
        public Service(AppCore.IOC.IContainer container, TDataSource dataSource)
            : base(container)
        {
            _dataSource = dataSource;
        }

        protected readonly TDataSource _dataSource;
    }
}
