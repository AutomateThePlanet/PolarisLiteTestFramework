namespace PolarisLite.Core.Infrastructure;
public static class InstanceFactory
{
    public static T Create<T>()
    {
        return (T)Activator.CreateInstance(typeof(T));
    }
}