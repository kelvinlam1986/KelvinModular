using Catalog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddCatalogModule(builder.Configuration);

var app = builder.Build();

// Configure HTTP request pipeline

app.Run();
