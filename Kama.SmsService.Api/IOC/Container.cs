using System;
using DryIoc;
using IDryContainer = DryIoc.IContainer;

namespace Kama.SmsService.IOC
{
    public class Container : AppCore.IOC.IContainer
    {
        public Container(IDryContainer container)
        {
            _container = container;
        }

        readonly IDryContainer _container;

        public void Dispose()
        {

        }

        public bool IsRegistered(Type t)
            => _container.IsRegistered(t);

        public void RegisterInstance<T>(T instance)
            => _container.RegisterInstance<T>(instance);

        public void RegisterInstance<T>(string name, T instance)
            => _container.RegisterInstance<T>(serviceKey: name, instance: instance);

        public void RegisterType<TFrom, TTo>()
            where TTo : TFrom
            => _container.Register<TFrom, TTo>();

        public void RegisterType<TFrom, TTo>(string name)
            where TTo : TFrom
            => _container.Register<TFrom, TTo>(serviceKey: name);

        public void RegisterType(Type t)
            => _container.Register(serviceAndMayBeImplementationType: t);

        public void RegisterType(string name, Type t)
            => _container.Register(serviceKey: name, serviceAndMayBeImplementationType: t);

        public T Resolve<T>()
            => _container.Resolve<T>();

        public object Resolve(Type t)
            => _container.Resolve(t);

        public void RegisterType(Type from, Type to)
            => _container.Register(serviceType: from, implementationType: to);

        public void RegisterType(Type from, Type to, string name)
            => _container.Register(serviceType: from, implementationType: to, serviceKey: name);

        public T TryResolve<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public object TryResolve(Type t)
        {
            try
            {
                return _container.Resolve(t);
            }
            catch
            {
                return null;
            }
        }

        public T Resolve<T>(string key)
            => _container.Resolve<T>(key);

        public T TryResolve<T>(string key)
        {
            try
            {
                return _container.Resolve<T>(key);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public object Resolve(Type t, string key)
            => _container.Resolve(t, key);

        public object TryResolve(Type t, string key)
        {
            try
            {
                return _container.Resolve(t, key);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool IsRegistered(Type t, string key)
            => _container.IsRegistered(t, key);
    }
}
