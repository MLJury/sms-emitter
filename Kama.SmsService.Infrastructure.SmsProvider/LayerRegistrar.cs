using System;
using Kama.AppCore.IOC;
using MAGFA = Kama.SmsService.Infrastructure.MagfaSms;
using Kama.SmsService.Core.MagfaSms;

[assembly: Registrar(typeof(MAGFA.LayerRegistrar))]
namespace Kama.SmsService.Infrastructure.MagfaSms
{
    using ASM = System.Reflection.Assembly;
    using svc = Core.MagfaSms;

    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            ASM asmInterfaces = ASM.GetAssembly(typeof(svc.IMagfaService)),
                asmClasses = ASM.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: asmInterfaces,
                implementationsAssembly: asmClasses,
                isService: t => t.IsInterface && !t.IsClass && typeof(svc.IMagfaService).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(MAGFA.Service))
                );
        }
    }
}
