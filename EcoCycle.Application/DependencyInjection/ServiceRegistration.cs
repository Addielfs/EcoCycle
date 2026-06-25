using EcoCycle.Application.Interfaces;
using EcoCycle.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcoCycle.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IPublicacionService, PublicacionService>();
            services.AddScoped<ICentroReciclajeService, CentroReciclajeService>();
            services.AddScoped<IRecompensaService, RecompensaService>();
            services.AddScoped<IConfiguracionService, ConfiguracionService>();
            services.AddScoped<IReporteService, ReporteService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);

            return services;
        }
    }
}
