using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crm.contact.api.Models;
using GraphQL.Types;

namespace crm.contact.api.Schema
{
    public class ContactType : ObjectGraphType<Contact>
    {
        public ContactType()
        {
            Field(c => c.ContactId);
        }
    }
}
