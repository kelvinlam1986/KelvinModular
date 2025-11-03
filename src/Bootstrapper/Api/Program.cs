using Carter;

using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBaksetModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

// Configure HTTP request pipeline
app.MapCarter();

app
  .UseCatalogModule()
  .UseBasketModule()
  .UseOrderingModule();


app.Run();
