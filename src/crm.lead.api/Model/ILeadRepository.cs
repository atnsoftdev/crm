using System.Collections.Generic;
using System.Threading.Tasks;
using LeadApi;

namespace CRM.Lead.Model
{
    public interface ILeadRepository
    {
        Task<IList<LeadInformation>> GetLeadsAsync();
    }
}