using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;
using EcoCycle.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoCycle.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ICentroReciclajeRepository, CentroReciclajeRepository>();
            services.AddScoped<IPublicacionRepository, PublicacionRepository>();
            services.AddScoped<IRecompensaRepository, RecompensaRepository>();
            services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();

            return services;
        }
    }
}
