using System;
using Kama.AppCore.IOC;
using DAL = Kama.SmsService.Infrastructure.DAL;

[assembly:Kama.AppCore.IOC.Registrar(typeof(DAL.LayerRegistrar))]
namespace Kama.SmsService.Infrastructure.DAL
{
    using ASM = System.Reflection.Assembly;
    using ds = Core.DataSource;

    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            ASM asmInterfaces = ASM.GetAssembly(typeof(ds.IDataSource)),
                asmClasses = ASM.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: asmInterfaces,
                implementationsAssembly: asmClasses,
                isService: t => t.IsInterface && !t.IsClass && typeof(ds.IDataSource).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(DAL.DataSource))
                );
        }
    }
}
