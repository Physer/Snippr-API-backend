using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snippr.Application.Data;
using Snippr.Application.Options;
using Snippr.Mongo;

namespace Snippr.Startup
{
    public static class ServiceConfigurationExtensions
    { 
        public static void ConfigureCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.ConfigurationEntry));
        }

        public static void ConfigureDataDependencies(this IServiceCollection services, IConfiguration configuration)
        { 
            // All transient services
            services.Scan(configuration => configuration.FromAssemblyOf<IDataDependency>().AddClasses(classes => classes.AssignableTo<IDataDependency>()).AsImplementedInterfaces().WithTransientLifetime());

            // Custom lifetime configuration
            services.AddSingleton<IRepository, MongoRepository>();
        }
    }
}
