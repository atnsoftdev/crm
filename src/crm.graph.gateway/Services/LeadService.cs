using crm.shared.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.ClientFactory;
using LeadApi;
using Microsoft.Extensions.Logging;

namespace crm.graph.gateway.Services
{
    public class LeadService : ServiceBase, ILeadService
    {
        private readonly LeadApi.Lead.LeadClient _leadClient;
        private readonly ILogger<LeadService> _logger;

        public LeadService(GrpcClientFactory clientFactory, ILoggerFactory loggerFactory) : base()
        {
            _leadClient = clientFactory.CreateClient<LeadApi.Lead.LeadClient>(nameof(Lead.LeadClient));
            _logger = loggerFactory.CreateLogger<LeadService>();
        }
        public LeadsResponse GetLeads()
        {     
            _logger.LogInformation("ssss");       
            var result = _leadClient.getLeads(new Empty());

            return result;
        }
    }
}