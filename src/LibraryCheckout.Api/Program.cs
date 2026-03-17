using LibraryCheckout.Api.Core.Extensions;
using LibraryCheckout.Api.Core.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

builder.Host.UseConfiguredSerilog();

builder.Services
    .AddApiServices()
    .AddApiDocumentation()
    .AddApiAuthorization()
    .AddApiValidation();

var app = builder.Build();

app.ConfigureApiPipeline();
app.MapApiEndpoints();

app.Run();
