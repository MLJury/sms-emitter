using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Infrastructure.DAL
{

    class DataSource
    {
        public DataSource(AppCore.IOC.IContainer container)
        {
            _container = container;
            _logger = _container?.TryResolve<Core.IEventLogger>();
            _objSerializer = _container?.TryResolve<AppCore.IObjectSerializer>();

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SmsService"]?.ConnectionString;
            _dbPublic = new PBL(connectionString);
            _dbMessage = new MSG(connectionString);

        }

        protected readonly AppCore.IOC.IContainer _container;
        protected readonly AppCore.IObjectSerializer _objSerializer;
        protected readonly Core.IEventLogger _logger;
        protected readonly PBL _dbPublic;
        protected readonly MSG _dbMessage;
    }
}
