using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace PolarisLite.Core;

public sealed class ConfigurationService
{
    private static readonly IConfigurationRoot Root = InitializeConfiguration();

    private static IConfigurationRoot InitializeConfiguration()
    {
        var filesInExecutionDir = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        var settingsFile = filesInExecutionDir.FirstOrDefault(x => x.Contains("testFrameworkSettings") && x.EndsWith(".json"));
        var builder = new ConfigurationBuilder();
        if (settingsFile != null)
        {
            builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
        }

        return builder.Build();
    }
}
