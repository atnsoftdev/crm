using CRM.Lead.Api.Services;
using CRM.Lead.Model;
using CRM.Shared.Interceptors;
using CRM.Shared.Jaeger;
using CRM.Shared.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Npgsql;
using OpenTracing.Contrib.Grpc.Interceptors;

namespace CRM.Lead.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            IdentityModelEventSource.ShowPII = true;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJaeger();

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<ServerTracingInterceptor>();
                options.Interceptors.Add<ExceptionInterceptor>();
                options.EnableDetailedErrors = true;
            });

            services.AddSingleton(new MaxConcurrentCallsInterceptor(200));

            // services.AddAuthorization();
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer((options) =>
            //     {
            //         options.Authority = "http://localhost:8080/auth/realms/master";
            //         options.RequireHttpsMetadata = false;
            //         options.Audience = "account";
            //     });

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddScoped<IUnitOfWork>(sp => {
                return new UnitOfWork(() => new NpgsqlConnection(Configuration.GetConnectionString("default")));
            });
            services.AddTransient<ILeadRepository, LeadRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Communication with gRPC endpoints must be made through a gRPC client.
                // To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
                endpoints.MapGrpcService<LeadService>();
            });
        }
    }
}
