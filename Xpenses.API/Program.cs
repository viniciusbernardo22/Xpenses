using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xpenses.API.Data;
using Xpenses.API.Endpoints;
using Xpenses.API.Handlers;
using Xpenses.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => options.CustomSchemaIds(n => n.FullName));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

// Open telemetry configuration //
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("Service-name"))
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation();
        metrics.AddHttpClientInstrumentation();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
            
        tracing.AddOtlpExporter();
    });
builder.Logging.AddOpenTelemetry(options => options.AddOtlpExporter());
// End of OpenTelemetry config //


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Ok");

app.MapEndpoints();

app.Run();

