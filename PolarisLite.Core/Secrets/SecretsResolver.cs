namespace PolarisLite.Secrets;

public static class SecretsResolver
{
    // extension method
    public static string GetSecretValue(this string configValue)
    {
        if (configValue.StartsWith("{env_"))
        {
            string environmentalVariable = Environment.GetEnvironmentVariable(configValue.Replace("env_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty), EnvironmentVariableTarget.Machine);
            return environmentalVariable;
        }
        else if (configValue.StartsWith("{vault_"))
        {
            string keyVaultValue = KeyVault.GetSecret(configValue.Replace("vault_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
            return keyVaultValue;
        }
        else if (configValue.StartsWith("{sm_"))
        {
            string secretValue = AwsSecretsManager.GetSecret(configValue.Replace("sm_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
            return secretValue;
        }
        else
        {
            return configValue;
        }
    }

    public static string GetSecret(Func<string> getConfigValue)
    {
        if (getConfigValue().StartsWith("{env_"))
        {
            string environmentalVariable = Environment.GetEnvironmentVariable(getConfigValue().Replace("env_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty), EnvironmentVariableTarget.Machine);
            return environmentalVariable;
        }
        else if (getConfigValue().StartsWith("{vault_"))
        {
            string keyVaultValue = KeyVault.GetSecret(getConfigValue().Replace("vault_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
            return keyVaultValue;
        }
        else if (getConfigValue().StartsWith("{sm_"))
        {
            string secretValue = AwsSecretsManager.GetSecret(getConfigValue().Replace("sm_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
            return secretValue;
        }
        else
        {
            return getConfigValue();
        }
    }

    public static string GetSecret(string name)
    {
        string environmentalVariable = Environment.GetEnvironmentVariable(name.Replace("env_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty), EnvironmentVariableTarget.Machine);
        if (!string.IsNullOrEmpty(environmentalVariable))
        {
            return environmentalVariable;
        }
        else if (KeyVault.IsAvailable)
        {
            string keyVaultValue = KeyVault.GetSecret(name.Replace("vault_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
            return keyVaultValue;
        }
        else if (AwsSecretsManager.IsAvailable)
        {
            string secretValue = AwsSecretsManager.GetSecret(name.Replace("sm_", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
            return secretValue;
        }
        else
        {
            throw new ArgumentException("You need to initialize an environmental variable or key vault secret first.");
        }
    }
}