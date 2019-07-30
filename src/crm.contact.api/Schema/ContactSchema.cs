using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;

namespace crm.contact.api.Schema
{
    public class ContactSchema : GraphQL.Types.Schema
    {
        public ContactSchema(ContactQuery query, IDependencyResolver resolver)
        {
            Query = query;
            DependencyResolver = resolver;
        }
    }
}
