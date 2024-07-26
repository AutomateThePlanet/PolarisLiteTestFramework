using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace PolarisLite.Core;

public sealed class ConfigurationService
{
    private static readonly IConfigurationRoot Root = InitializeConfiguration();

    public static TSection GetSection<TSection>()
        where TSection : class, new()
    {
        string sectionName = typeof(TSection).Name.MakeFirstLetterToLower();
        return Root.GetSection(sectionName).Get<TSection>();
    }

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
