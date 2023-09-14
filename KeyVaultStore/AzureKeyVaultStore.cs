using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace KeyVaultStore
{
    public class AzureKeyVaultStore : IKeyValueStore
    {

        private const string _baseUrl = "";
        public async Task<string> Get(string key, CancellationToken ct)
        {
            Console.WriteLine("Get secret '{0}' from Azure Key Vault {1}", key, _baseUrl);
            var keyVaultClient = new SecretClient(new Uri(_baseUrl), new DefaultAzureCredential());
            try
            {
                var secretResponse = await keyVaultClient.GetSecretAsync(key, null, ct);
                var secret = secretResponse.Value;
                Console.WriteLine("Got secret from vault");
                return secret.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get secret '{0}' from Azure Ket Vault {1} with error {2}", key, _baseUrl, ex.Message);
            }
            return string.Empty;
        }

        public async Task<bool> Store(string key, string value, CancellationToken ct)
        {
            Console.WriteLine("Store secret '{0}' in Azure Key Vault {1}", key, _baseUrl);
            var keyVaultClient = new SecretClient(new Uri(_baseUrl), new DefaultAzureCredential());
            try
            {
                var secret = new KeyVaultSecret(key, value);
                await keyVaultClient.SetSecretAsync(secret, ct);
                Console.WriteLine("Stored secret in vault");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to store secret '{0}' in Azure Key Vault {1} with error {2}", key, _baseUrl, ex.Message);
            }
            return false;
        }

        public async Task<bool> Remove(string key, CancellationToken ct)
        {
            Console.WriteLine("Remove secret '{0}' from Azure Key Vault {1}", key, _baseUrl);
            var keyVaultClient = new SecretClient(new Uri(_baseUrl), new DefaultAzureCredential());
            try
            {
                await keyVaultClient.StartDeleteSecretAsync(key, ct);
                Console.WriteLine("Deleted secret from keyvault");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete secret '{0}' from Azure Vault {1} with error {2}", key, _baseUrl, ex.Message);
            }
            return false;
        }
    }
}