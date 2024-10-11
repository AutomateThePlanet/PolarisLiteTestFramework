using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using PolarisLite.Core;

namespace PolarisLite.Secrets;

public static class KeyVault
{
    private static SecretClient _secretClient;
    // A private readonly object (_lockObject) is added for synchronization, ensuring that multiple threads do not attempt to initialize the _secretClient or access shared resources simultaneously.
    private static readonly object _lockObject = new object(); // Lock object for thread safety

    static KeyVault()
    {
        InitializeClient();
    }


    // The IsAvailable property now checks the _secretClient within a lock block to ensure consistent and thread-safe access.
    public static bool IsAvailable
    {
        get
        {
            lock (_lockObject)
            {
                return _secretClient != null;
            }
        }
    }

    // Ensures that concurrent access to GetSecret and IsAvailable is safe and will not cause race conditions or inconsistent states.
    public static string GetSecret(string name)
    {
        lock (_lockObject) // Ensure thread-safe access to _secretClient
        {
            if (_secretClient == null)
            {
                return null;
            }

            var secret = _secretClient.GetSecret(name);
            return secret.Value.Value;
        }
    }

    // In the InitializeClient method, double-checked locking is used to prevent multiple threads from initializing _secretClient at the same time. Even if multiple threads call the InitializeClient method, only one thread will initialize the _secretClient, while others will skip the initialization after it's set.
    private static void InitializeClient()
    {
        // Use double-checked locking to ensure thread safety and prevent multiple initializations
        if (_secretClient == null)
        {
            lock (_lockObject)
            {
                if (_secretClient == null)
                {
                    var settings = ConfigurationService.GetSection<KeyVaultSettings>();
                    if (settings != null && settings.IsEnabled && !string.IsNullOrEmpty(settings.KeyVaultEndpoint))
                    {
                        // Create a new secret client using credentials from environment variables or managed identity
                        var cred = new ChainedTokenCredential(new ManagedIdentityCredential(), new AzureCliCredential());
                        _secretClient = new SecretClient(vaultUri: new Uri(settings.KeyVaultEndpoint), cred);
                    }
                }
            }
        }
    }
}
