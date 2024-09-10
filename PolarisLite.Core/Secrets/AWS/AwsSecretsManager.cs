using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using PolarisLite.Core.Settings.StaticSettings;

namespace PolarisLite.Secrets;

public static class AwsSecretsManager
{
    private static IAmazonSecretsManager _secretsManagerClient;

    static AwsSecretsManager()
    {
        InitializeClient();
    }

    public static bool IsAvailable => _secretsManagerClient != null;

    public static async Task<string> GetSecret(string secretName)
    {
        if (_secretsManagerClient == null)
        {
            return null;
        }

        try
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            var response = await _secretsManagerClient.GetSecretValueAsync(request);

            if (response.SecretString != null)
            {
                return response.SecretString;
            }
            else
            {
                // The secret could be a binary secret, handle accordingly if needed.
                return Convert.ToBase64String(response.SecretBinary.ToArray());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving secret: {e.Message}");
            return null;
        }
    }

    private static void InitializeClient()
    {
        if (_secretsManagerClient == null)
        {
            if (GlobalSettings.AwsSecretsSettings.IsEnabled)
            {
                _secretsManagerClient = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(GlobalSettings.AwsSecretsSettings.Region));
            }
        }
    }
}
