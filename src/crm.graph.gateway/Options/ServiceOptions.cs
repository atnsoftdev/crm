namespace crm.graph.gateway.Options
{
    public class ServiceOptions
    {
        public ServiceConfig LeadService { get; set; }
    }

    public class ServiceConfig
    {
        public string ServiceName { get; set; }
        public string Url { get; set; }
    }
}