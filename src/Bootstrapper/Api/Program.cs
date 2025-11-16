var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container

// Common services: Carter, MediatR, Fluent Validation
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

builder.Services.AddCarterWithAssemblies(catalogAssembly, basketAssembly);
builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// modules services: Catalog, Basket, Ordering
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBaksetModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure HTTP request pipeline
app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app
  .UseCatalogModule()
  .UseBasketModule()
  .UseOrderingModule();

app.Run();
