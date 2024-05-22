using Application.Services;

namespace CitenikBank.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IClienteServices, ClientesServices>();
            services.AddTransient<IPlanesServices, PlanesServices>();
            services.AddTransient<IUsuariosServices, UsuariosServices>();
            services.AddTransient<IPrestamosServices, PrestamosServices>();
            services.AddTransient<ICuotaServices, CuotaServices>();
            services.AddTransient<IPagosServices, PagosServices>();
            services.AddTransient<IEstadisticasServices, EstadisticasServices>();
            return services;
        }
    }
}
