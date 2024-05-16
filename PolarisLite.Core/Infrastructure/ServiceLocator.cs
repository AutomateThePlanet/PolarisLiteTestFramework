using Unity;
using System;

namespace PolarisLite.Core;

public class ServiceLocator
{
    private static readonly Lazy<ServiceLocator> _instance =
        new Lazy<ServiceLocator>(() => new ServiceLocator(), true);
    private readonly IUnityContainer _container;

    private ServiceLocator()
    {
        _container = new UnityContainer();
    }

    public static ServiceLocator Instance => _instance.Value;

    public void RegisterInstance<T>(T service)
    {
        _container.RegisterInstance(typeof(T), Guid.NewGuid().ToString(), service);
    }

    public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
    {
        _container.RegisterType<TInterface, TImplementation>();
    }

    public IEnumerable<T> GetAllServices<T>()
    {
        return _container.ResolveAll<T>();
    }

    public T GetService<T>()
    {
        return _container.Resolve<T>();
    }
}
