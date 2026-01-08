using Main.Pipelines;
using MainMessaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace OrderingApplication
{
    public static class DIContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationPipeline<,>));
                cfg.AddOpenBehavior(typeof(LoggerPipeline<,>));
            });

            services.AddFeatureManagement();

            services.AddMessageBroker(config, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
 