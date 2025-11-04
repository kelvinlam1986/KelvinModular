using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Shared.Data;
using Shared.Data.Seed;
using Catalog.Data.Seed;
using Shared.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Behaviors;


namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection service, 
            IConfiguration configuration)
        {
            // Add services to the container
            // Api Endpoint services

            // Application use case services
            service.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");
            service.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            service.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            service.AddDbContext<CatalogDbContext>((sp, options) =>
                {
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                    options.UseNpgsql(connectionString);
                }
            );
            service.AddScoped<IDataSeeder, CatalogDataSeeder>();
            return service;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline

            // Use API Endpoint service

            // Use Application service usecase

            // Use Data - Infrastructure service
            app.UseMigration<CatalogDbContext>();

            return app;
        }

    }
}
