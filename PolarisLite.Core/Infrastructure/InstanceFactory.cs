namespace PolarisLite.Core.Infrastructure;
public static class InstanceFactory
{
    public static T Create<T>()
    {
        try
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default;
        }
    }

    public static T Create<T>(params object[] args)
    {
        try
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default;
        }
    }

    public static T CreateByTypeParameter<T>(Type parameterClass, int index)
    {
        try
        {
            var genericType = parameterClass.BaseType.GetGenericArguments()[index];
            return (T)Activator.CreateInstance(genericType);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default;
        }
    }
}