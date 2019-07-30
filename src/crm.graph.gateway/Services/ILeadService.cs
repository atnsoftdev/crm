using System.Collections.Generic;

namespace crm.graph.gateway.Services
{
    public interface ILeadService
    {
        LeadApi.LeadsResponse GetLeads();
    }
}