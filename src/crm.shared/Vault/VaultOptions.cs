namespace CRM.Shared.Vault
{
    public class VaultOptions
    {
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public int? Version { get; set; }
        public string MountPoint { get; set; } = "kv";
        public string AuthType { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}