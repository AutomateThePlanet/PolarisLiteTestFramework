using System.Diagnostics;
using System.Reflection;

namespace PolarisLite.Core.Utilities;

public static class EmbeddedResourcesService
{
    private static Assembly _currentExecutingAssembly;

    public static string FromFile(string name, string fileExtension)
    {
        var resourceName = GetResourceByFileName($"{name}.{fileExtension}");
        return FromFile(resourceName);
    }

    public static string FromFile(string name)
    {
        _currentExecutingAssembly ??= GetAssembliesCallChain()[2];
        string currentFileTempPath;
        using (var resourceStream = _currentExecutingAssembly.GetManifestResourceStream(name))
        {
            var tempFolder = Path.GetTempPath();
            currentFileTempPath = Path.Combine(tempFolder, name).Replace("\\", "/");

            using Stream file = File.Create(currentFileTempPath);
            CopyStream(resourceStream, file);
        }

        if (!File.Exists(currentFileTempPath))
        {
            throw new ArgumentException($"Image {name} was not found. Please add the base line image as embedded resource.");
        }

        return currentFileTempPath;
    }

    private static string GetResourceByFileName(string name)
    {
        try
        {
            _currentExecutingAssembly ??= GetAssembliesCallChain()[2];
            var resourceName = _currentExecutingAssembly.GetManifestResourceNames().First(str => str.EndsWith(name));
            return resourceName;
        }
        catch (InvalidOperationException e)
        {
            throw new InvalidOperationException($"Resource with name = {name} was not found. Please make sure to include the resource and set it as embedded resource to the test project.", e);
        }
    }

    private static void CopyStream(Stream input, Stream output)
    {
        var buffer = new byte[8 * 1024];
        int len;
        while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, len);
        }
    }

    private static List<Assembly> GetAssembliesCallChain()
    {
        var trace = new StackTrace();
        var assemblies = new List<Assembly>();
        var frames = trace.GetFrames();

        if (frames == null)
        {
            throw new Exception("Couldn't get the stack trace");
        }

        foreach (var frame in frames)
        {
            var method = frame.GetMethod();
            var declaringType = method.DeclaringType;

            if (declaringType == null)
            {
                continue;
            }

            var declaringTypeAssembly = declaringType.Assembly;
            var lastAssembly = assemblies.LastOrDefault();

            if (declaringTypeAssembly != lastAssembly)
            {
                assemblies.Add(declaringTypeAssembly);
            }
        }

        foreach (var currentAssembly in assemblies)
        {
            Debug.WriteLine(currentAssembly.ManifestModule.Name);
        }

        return assemblies;
    }
}