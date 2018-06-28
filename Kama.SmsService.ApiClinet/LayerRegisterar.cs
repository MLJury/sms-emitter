using System;
using Kama.AppCore.IOC;

[assembly: Registrar(typeof(Kama.SmsService.ApiClient.LayerRegistrar))]
namespace Kama.SmsService.ApiClient
{
    class LayerRegistrar : IRegistrar
    {
        readonly Guid _id = Guid.NewGuid();
        public Guid ID => _id;

        public void Start(IContainer container)
        {
            var clientAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: clientAssembly,
                implementationsAssembly: clientAssembly,
                isService: t => t.IsInterface && t != typeof(Interface.IService) && typeof(Interface.IService).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(Service))
                );

            var hostInfo = container.TryResolve<Interface.ISmsServiceHostInfo>();
            var objectSerializer = container.TryResolve<AppCore.IObjectSerializer>();

            container.RegisterInstance<Interface.ISmsServiceClient>(new SmsServiceClient(objectSerializer, hostInfo.Host, hostInfo.GetDefaultHeaders));
        }
    }
}
