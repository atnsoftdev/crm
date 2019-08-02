using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Shared.Repository;
using Dapper;
using LeadApi;
using OpenTracing;
using OpenTracing.Tag;

namespace CRM.Lead.Model
{
    public class LeadRepository : ILeadRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ITracer _tracer;

        public LeadRepository(IUnitOfWork uow, ITracer tracer)
        {
            _uow = uow;
            _tracer = tracer;
        }
        public async Task<IList<LeadInformation>> GetLeadsAsync()
        {
            using (var tracingScope = _tracer.BuildSpan(this.ToString()).StartActive(finishSpanOnDispose: true))
            {
                var sql = @"SELECT leadowner, leadid, firstname, lastname, company, email, title, 
                            description, cast(1 as decimal) addressid, street, city, zipcode, state, country
                        FROM lead";

                tracingScope.Span.Log("Processing method: 'GetLeadsAsync'");
                tracingScope.Span.SetTag(Tags.DbStatement, sql);

                var leads = await _uow.Connection.QueryAsync<LeadInformation, AddressInformation, LeadInformation>(sql, (lead, address) =>
                {
                    lead.Address = address;
                    return lead;
                }, splitOn: "addressid");

                return leads.AsList();
            }
        }
    }
}