using Unity;
using Unity.Lifetime;

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
        _container.RegisterInstance(typeof(T), Guid.NewGuid().ToString(), service, new ContainerControlledLifetimeManager());
    }

    public void UnregisterInstance<TFrom>()
    {
        var registration = _container.Registrations.FirstOrDefault(r => r.RegisteredType.Equals(typeof(TFrom)));
        registration?.LifetimeManager?.RemoveValue();
    }

    public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
    {
        _container.RegisterType<TInterface, TImplementation>();
    }

    public IEnumerable<T> GetAllServices<T>()
         where T : class
    {
        return _container.ResolveAll<T>();
    }

    public T GetService<T>()
        where T : class
    {
        try
        {
            return _container.Resolve<T>();
        }
        catch (Exception ex)
        {
            return null;
        }
        //if (!_container.Registrations.Any(r => r.RegisteredType.Equals(typeof(T))))
        //{
        //    return null;
        //}

        
    }
}
