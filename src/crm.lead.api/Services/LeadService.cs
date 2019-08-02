using System.Threading.Tasks;
using CRM.Lead.Model;
using CRM.Shared.ValidationModel;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LeadApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using static LeadApi.Lead;

namespace CRM.Lead.Api.Services
{
    public class LeadService : LeadBase
    {
        private readonly ILogger<LeadService> _logger;
        private readonly IValidator<CreateLeadRequest> _validator;
        private readonly ILeadRepository _leadRepo;

        public LeadService(ILoggerFactory loggerFactory,
            IValidator<CreateLeadRequest> validator,
            ILeadRepository leadRepository,
            Shared.EventBus.Nats.INatsConnection connection)
        {
            _logger = loggerFactory.CreateLogger<LeadService>();
            _validator = validator;
            _leadRepo = leadRepository;

            connection.TryConnect();
        }

        public override Task<PongReply> Ping(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new PongReply
            {
                Message = "Pong"
            });
        }

        // [Authorize]
        public override async Task<LeadsResponse> getLeads(Empty request, ServerCallContext context)
        {
            var leads = await _leadRepo.GetLeadsAsync();
            var response = new LeadsResponse();
            response.Leads.AddRange(leads);

            return response;
        }

        public override async Task<LeadInformation> createLead(CreateLeadRequest request, ServerCallContext context)
        {
            await _validator.HandleValidation(request);
            return new LeadInformation();
        }
    }
}
