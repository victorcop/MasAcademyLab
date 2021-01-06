using MasAcademyLab.Data.Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasAcademyLab.Service.Extention
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDependencies(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDataDependencies(configuration);
            services.AddScoped<ISpeakerService, SpeakerService>();
            return services;
        }
    }
}
