using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crm.contact.api.Models
{
    public class Contact
    {
        public Contact(Guid contactId)
        {
            ContactId = contactId;
        }

        public Guid ContactId { get; set; }
    }
}
