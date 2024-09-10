using PolarisLite.Integrations.Settings;
using PolarisLite.Secrets;

namespace PolarisLite.Web.Configuration.StaticImplementation;
public class IntegrationsConfigurationLoader
{
    public static void LoadConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        switch (environment)
        {
            case "Development":
                LoadDevelopmentConfiguration();
                break;
            case "Staging":
                LoadStagingConfiguration();
                break;
            case "QA":
                LoadQAConfiguration();
                break;
            default:
                throw new InvalidOperationException($"Unknown environment: {environment}");
        }
    }

    private static void LoadQAConfiguration()
    {
        //IntegrationSettings.BlobStorageSettings.ConnectionString = Environment.GetEnvironmentVariable("BLOB_CS");
        IntegrationSettings.BlobStorageSettings.ConnectionString = SecretsResolver.GetSecret("BLOB_CS");
    }

    private static void LoadStagingConfiguration()
    {
        //IntegrationSettings.BlobStorageSettings.ConnectionString = Environment.GetEnvironmentVariable("BLOB_CS");
        IntegrationSettings.BlobStorageSettings.ConnectionString = SecretsResolver.GetSecret("BLOB_CS");
    }

    private static void LoadDevelopmentConfiguration()
    {
        IntegrationSettings.BlobStorageSettings.ConnectionString = SecretsResolver.GetSecret("BLOB_CS");
        IntegrationSettings.AppInsightsSettings.ConnectionString = SecretsResolver.GetSecret("APPINSIGHTS_CS");
        IntegrationSettings.AppInsightsSettings.IsEnabled = true;
        IntegrationSettings.AzureDevOpsBuildUrl = SecretsResolver.GetSecret("AzureDevOpsBuildUrl");
        IntegrationSettings.MailslurpSettings.ApiKey = "someKey";
        IntegrationSettings.TwilioSettings.AuthToken = "someAuthToken";
        IntegrationSettings.TwilioSettings.PhoneNumber = "+12312312321312";
        IntegrationSettings.TwilioSettings.AccountSID = "someAccountSID";
        IntegrationSettings.TwoFASecret = "d#IryuziNby|$z(E<+SW>Gl*Elg{|%";
    }
}
