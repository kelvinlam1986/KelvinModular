using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;


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

            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");
            service.AddDbContext<CatalogDbContext>(options => 
                options.UseNpgsql(connectionString));

            return service;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            InitialiseDatabaseAsync(app).GetAwaiter().GetResult();  
            return app;
        }

        private static async Task InitialiseDatabaseAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
