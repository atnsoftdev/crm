using crm.graph.gateway.Options;
using crm.shared.Services;
using CRM.Graph.Gateway.Query;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using LeadApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using CRM.Shared;

namespace CRM.Graph.Gateway
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            GraphQLRegister(services);

            GrpcRegister(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseGraphQL<ISchema>("/graphql");
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
            {
                Path = "/ui/playground"
            });
        }

        // ReSharper disable once InconsistentNaming
        private void GraphQLRegister(IServiceCollection services)
        {
            services.Scan(scan => scan.FromCallingAssembly()
                .AddClasses(x => x.AssignableTo(typeof(ObjectGraphType<>)))
                .AsSelf()
                .WithScopedLifetime());

            services.AddScoped<CrmQuery>();
            services.AddScoped<ISchema, CrmSchema>();
            services.AddScoped<IDependencyResolver>(c => new FuncDependencyResolver(c.GetRequiredService));

            services.AddGraphQL(o =>
            {
                // o.EnableMetrics = true;
                o.ExposeExceptions = true;
            });
        }

        private void GrpcRegister(IServiceCollection services)
        {
            services.Scan(scan => scan
                            .FromCallingAssembly()
                            .AddClasses(x => x.AssignableTo(typeof(ServiceBase)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());

            var serviceOptions = Configuration.GetOptions<ServiceOptions>("Services");

            services.AddGrpcClient<Lead.LeadClient>(o =>
            {
                o.BaseAddress = new Uri(serviceOptions.LeadService.Url);
            });
        }
    }
}
