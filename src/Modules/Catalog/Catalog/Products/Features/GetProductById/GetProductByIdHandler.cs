

namespace Catalog.Products.Features.GetProductById
{
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(query.Id);
            }

            var productDto = product.Adapt<ProductDto>();
            return new GetProductByIdResult(productDto);
        }
    }
}
