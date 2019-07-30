using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.AuthMethods.UserPass;

namespace CRM.Shared.Vault
{
    public class VaultStore : IVaultStore
    {
        private readonly VaultOptions _options;
        public VaultStore(VaultOptions options)
        {
            _options = options;
            LoadEnvironmentVariables();
        }
        public async Task<IDictionary<string, object>> GetDefaultAsync()
        {
            if (string.IsNullOrWhiteSpace(_options.Key))
            {
                throw new VaultException("Vault secret key can not be empty.");
            }
            try
            {
                var authMethodInfo = GetAuthMethod();
                var settings = new VaultClientSettings(_options.Url, authMethodInfo);
                var client = new VaultClient(settings);
                var secret = await client.V1.Secrets.KeyValue.V2.ReadSecretAsync(_options.Key, _options.Version, _options.MountPoint);

                return secret.Data.Data;
            }
            catch (Exception ex)
            {
                throw new VaultException($"Getting Vault secret for key: '{_options.Key}' caused an error" + $"{ex.Message}", ex, _options.Key);
            }
        }

        private IAuthMethodInfo GetAuthMethod()
        {
            switch (_options.AuthType?.ToLowerInvariant())
            {
                case "token":
                    return new TokenAuthMethodInfo(_options.Token);
                case "userpass":
                    return new UserPassAuthMethodInfo(_options.Username, _options.Password);
                default:
                    throw new VaultAuthTypeNotSupportedException($"Vault auth type: '{_options.AuthType}' is not supported", _options.AuthType);
            }
        }

        private void LoadEnvironmentVariables()
        {
            _options.Url = GetEnvVariableValue("VAULT_URL") ?? _options.Url;
            _options.Key = GetEnvVariableValue("VAULT_KEY") ?? _options.Key;
            _options.AuthType = GetEnvVariableValue("VAULT_AUTH_TYPE") ?? _options.AuthType;
            _options.Token = GetEnvVariableValue("VAULT_TOKEN") ?? _options.Token;
            _options.Username = GetEnvVariableValue("VAULT_USER_NAME") ?? _options.Username;
            _options.Password = GetEnvVariableValue("VAULT_PASSWORD") ?? _options.Password;            
            _options.MountPoint = GetEnvVariableValue("VAULT_MOUNT_POINT") ?? _options.MountPoint;            
        }

        private string GetEnvVariableValue(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);

            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}