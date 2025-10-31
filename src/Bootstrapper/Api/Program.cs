using Catalog;
using Basket;
using Ordering;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBaksetModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

// Configure HTTP request pipeline
app
  .UseCatalogModule()
  .UseBasketModule()
  .UseOrderingModule();


app.Run();
