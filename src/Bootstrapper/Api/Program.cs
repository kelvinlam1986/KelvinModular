using FluentValidation;

using Shared.Behaviors;

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

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(catalogAssembly, basketAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblies([catalogAssembly, basketAssembly]);

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
