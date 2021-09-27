using Api.Models;
using Api.Services;
using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class StartupExtentions
    {
        public static IServiceCollection CongigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ILogStoreService, LogStoreService>();
            services.AddTransient<MailService>();

            return services;
        }
    }
}
