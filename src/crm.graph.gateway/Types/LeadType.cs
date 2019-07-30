using GraphQL.Types;

namespace CRM.Graph.Gateway.Types
{
    public class LeadType : ObjectGraphType<LeadApi.LeadInformation>
    {
        public LeadType()
        {
            Field(l => l.LeadId, nullable:false).Description("The id of lead");
            Field(l=>l.Address, type:typeof(AddressType));
        }
    }
}

