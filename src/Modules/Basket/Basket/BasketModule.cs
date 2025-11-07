using Basket.Data.Repository;

using Shared.Data;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBaksetModule(this IServiceCollection service, 
            IConfiguration configuration)
        {
            // Add services to the container
            // Api Endpoint services

            // Application use case services
            service.AddScoped<IBasketRepository, BasketRepository>();

            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");
            service.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            service.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            service.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            }
            );

            return service;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {

            // Configure the HTTP request pipeline

            // Use API Endpoint service

            // Use Application service usecase

            // Use Data - Infrastructure service
            app.UseMigration<BasketDbContext>();


            return app;
        }
    }
}
