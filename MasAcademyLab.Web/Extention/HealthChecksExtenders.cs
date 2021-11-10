using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasAcademyLab.Web.Extention
{
    public static class HealthChecksExtenders
    {
        public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecksUI().AddInMemoryStorage();
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("MasAcademyLab"))
                .AddProcessAllocatedMemoryHealthCheck(512); // 512 MB max allocated memory;
        }

        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app.UseHealthChecksUI(setup =>
            {
                setup.UIPath = "/show-health-ui"; // this is ui path in your browser
                setup.ApiPath = "/health-ui-api"; // the UI ( spa app )  use this path to get information from the store ( this is NOT the healthz path, is internal ui api )
            });
        }
    }
}
