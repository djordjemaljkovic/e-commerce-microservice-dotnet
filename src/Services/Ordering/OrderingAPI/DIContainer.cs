using Carter;
using Main.Exceptions.Handler;

namespace OrderingAPI
{
    public static class DIContainer
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCarter();

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks();

            return services;
        }

        public static WebApplication  UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(opt => { });
            app.UseHealthChecks("/health");

            return app;
        }
    }
}
