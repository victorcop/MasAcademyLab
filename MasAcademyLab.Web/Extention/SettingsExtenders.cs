using MasAcademyLab.Service.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MasAcademyLab.Web.Extention
{
    public static class SettingsExtenders
    {
        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddSingleton<IJwtSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value);
        }
    }
}
