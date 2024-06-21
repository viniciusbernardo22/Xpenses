using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xpenses.API.Data;
using Xpenses.API.Handlers;
using Xpenses.Core.Handlers;

namespace Xpenses.API;

public static class BuilderConfiguration
{
    public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("Default");
        /* Services */
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( options => options.CustomSchemaIds(n => n.FullName));
        services.AddTransient<ICategoryHandler, CategoryHandler>();
        services.AddTransient<ITransactionHandler, TransactionHandler>();
// Open telemetry configuration //
        services.AddOpenTelemetry()
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
        services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();
/* End of Services */
    }
}