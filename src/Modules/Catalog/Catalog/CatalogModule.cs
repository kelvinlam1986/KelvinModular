using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection service, 
            IConfiguration configuration)
        {
            return service;
        }
    }
}
