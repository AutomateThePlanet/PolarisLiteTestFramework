using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace PolarisLite.Core;

public sealed class ConfigurationService
{
    // private static readonly IConfigurationRoot Root = InitializeConfiguration();

    // Lazy<T> provides built-in locking mechanisms to ensure that initialization happens only once, even in multi-threaded environments.
    private static readonly Lazy<IConfigurationRoot> Root = new Lazy<IConfigurationRoot>(() => InitializeConfiguration(), isThreadSafe: true);

    // Once the IConfigurationRoot is initialized, access to the configuration is fast, without additional locking overhead.
    public static TSection GetSection<TSection>()
        where TSection : class, new()
    {
        string sectionName = typeof(TSection).Name.MakeFirstLetterToLower();
        return Root.Value.GetSection(sectionName).Get<TSection>(); // Access the configuration safely through Lazy.Value
    }

    // Configuration initialization will only happen when it is accessed for the first time, potentially improving performance if the configuration is not always needed.
    private static IConfigurationRoot InitializeConfiguration()
    {
        string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        string settingsFileName = $"testFrameworkSettings.{environment}.json";

        var builder = new ConfigurationBuilder();
        var executionDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var settingsFilePath = Path.Combine(executionDir, settingsFileName);

        if (File.Exists(settingsFilePath))
        {
            builder.AddJsonFile(settingsFilePath, optional: true, reloadOnChange: true);
        }
        else
        {
            throw new FileNotFoundException($"Configuration file '{settingsFileName}' not found in '{executionDir}'.");
        }

        return builder.Build();
    }
}

