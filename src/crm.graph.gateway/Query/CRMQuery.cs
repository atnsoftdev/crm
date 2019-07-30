using System;
using CRM.Graph.Gateway.Types;
using GraphQL.Types;
using LeadApi;

namespace CRM.Graph.Gateway.Query
{
    public class CrmQuery : ObjectGraphType
    {
        public CrmQuery()
        {
            Name = "Query";

            Field<LeadGroupGraphType>("leadQuery", resolve: context => new { });
        }
    }
}
