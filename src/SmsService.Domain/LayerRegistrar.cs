using System;
using AppCore.IOC;
using DMN = SmsService.Domain;
using ioc = AppCore.IOC;
using Config = System.Configuration.ConfigurationManager;
using SmsService.Core.Service;
using System.Timers;

[assembly:AppCore.IOC.Registrar(typeof(SmsService.Domain.LayerRegistrar), Order = 1)]
namespace SmsService.Domain
{
    using ASM = System.Reflection.Assembly;
    using svc = Core.Service;

    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            ASM asmInterfaces = ASM.GetAssembly(typeof(svc.IService)),
                asmClasses = ASM.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: asmInterfaces,
                implementationsAssembly: asmClasses,
                isService: t => t.IsInterface && !t.IsClass && typeof(svc.IService).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(DMN.Service))
                );

            //container.RegisterType<Core.Service.ITimer, MainTimer>();

            //start automation timer
            StartTimers(container);
        }

        private void StartTimers(ioc.IContainer container)
        {
            Timer _timer;
            _timer = new Timer(Convert.ToInt32(Config.AppSettings["AutomationTimerInterval"]) * 1000);
            ElapsedEventHandler triggerMethod = null;
            triggerMethod += container.Resolve<ISendService>().DoOnMainTimer;
            _timer.Elapsed += triggerMethod;
            if (Config.AppSettings["ServerType"] == "Internet")
                _timer.Start();
        }
    }
}
