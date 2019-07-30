using System;

namespace CRM.Shared.Vault
{
    public class VaultAuthTypeNotSupportedException : Exception
    {
        public string AuthType { get; private set; }

        public VaultAuthTypeNotSupportedException(string authType) : this(string.Empty, authType)
        {

        }

        public VaultAuthTypeNotSupportedException(string message, string authType) : base(message)
        {
            AuthType = authType;
        }
    }
}