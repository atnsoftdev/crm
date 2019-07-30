using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Shared.Repository;
using Dapper;
using LeadApi;

namespace CRM.Lead.Model
{
    public class LeadRepository : ILeadRepository
    {
        private readonly IUnitOfWork _uow;

        public LeadRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IList<LeadInformation>> GetLeadsAsync()
        {
            var sql = @"SELECT leadowner, leadid, firstname, lastname, company, email, title, 
                            description, cast(1 as decimal) addressid, street, city, zipcode, state, country
                        FROM lead";

            var leads = await _uow.Connection.QueryAsync<LeadInformation, AddressInformation, LeadInformation>(sql, (lead, address) =>
            {
                lead.Address = address;
                return lead;
            }, splitOn: "addressid");

            return leads.AsList();
        }
    }
}