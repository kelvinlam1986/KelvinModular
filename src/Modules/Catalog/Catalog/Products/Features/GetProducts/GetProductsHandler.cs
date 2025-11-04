namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(PaginationRequest Request)
        : IQuery<GetProductsResult>;

    public record GetProductsResult(PaginatedResult<ProductDto> Products);

    internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.Request.PageIndex;
            var pageSize = query.Request.PageSize;

            var count = await dbContext.Products.LongCountAsync();

            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize) 
                .ToListAsync(cancellationToken);

            var productDtos = products.Adapt<List<ProductDto>>();
            return new GetProductsResult(
                new PaginatedResult<ProductDto>(pageIndex, pageSize, count, productDtos)
                );
        }
    }
}
