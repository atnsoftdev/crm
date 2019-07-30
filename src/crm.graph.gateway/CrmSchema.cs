using CRM.Graph.Gateway.Query;
using GraphQL;
using GraphQL.Types;

namespace CRM.Graph.Gateway
{
    public class CrmSchema : Schema
    {
        public CrmSchema(IDependencyResolver resolver)
        {
            Query = resolver.Resolve<CrmQuery>();
            DependencyResolver = resolver;
        }
    }
}
