using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Shared.Vault
{
    public interface IVaultStore
    {
         Task<IDictionary<string, object>> GetDefaultAsync();
    }
}