using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crm.contact.api.Models;
using GraphQL.Types;

namespace crm.contact.api.Schema
{
    public class ContactQuery : ObjectGraphType
    {
        private IList<Contact> _contacts = new List<Contact>();

        public ContactQuery()
        {
            Name = "Query";
            FieldAsync<ListGraphType<ContactType>>("contacts", resolve: async context => await GetContactsAsync());
        }

        public Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return Task.FromResult(_contacts.AsEnumerable());
        }
    }
}
