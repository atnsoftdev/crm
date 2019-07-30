using GraphQL.Types;

namespace CRM.Graph.Gateway.Types
{
    public class AddressType : ObjectGraphType<LeadApi.AddressInformation>
    {
        public AddressType()
        {
            Field(f => f.City);
            Field(f => f.Country);
        }
    }
}

