using DryIoc;
using DryIoc.WebApi;
using System;
using DryContainer = DryIoc.Container;
using IDryContainer = DryIoc.IContainer;

namespace Kama.SmsService.IOC
{
    class Activator
    {
        static readonly Lazy<Activator> _instance = new Lazy<Activator>(() => new Activator());

        private static IDryContainer _container;

        private void RegisterTools()
        {
           _container.Register<AppCore.IObjectSerializer, Tools.ObjectSerializer>();
           _container.Register<Core.IEventLogger, Tools.Logger>();
        }

        public static AppCore.IOC.IContainer Container { get; private set; }

        public static Activator Instance => _instance.Value;

        public AppCore.IOC.IContainer ActiveWebApi(System.Web.Http.HttpConfiguration config, System.Reflection.Assembly[] webApiAssemblies)
        {
            _container = new DryContainer().WithWebApi(config, webApiAssemblies);
            _container.RegisterInstance<AppCore.IOC.IContainer>(new Container(_container));
            Container = _container.Resolve<AppCore.IOC.IContainer>();
            RegisterTools();

            return Container;
        }

        public void Deactivate()
            => _container.Dispose();
    }
}
