using Catalog.Products.Dtos;

using Shared.CQRS;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery()
        : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<ProductDto> Products);

    internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            var productDtos = ProjectToProductDto(products);
            return new GetProductsResult(productDtos);
        }

        private List<ProductDto> ProjectToProductDto(List<Product> products)
        {
            foreach (var product in products)
            {

            }

            return [];
        }
    }
}
