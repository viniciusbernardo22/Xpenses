using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xpenses.API.Data;
using Xpenses.API.Handlers;
using Xpenses.API.Models;
using Xpenses.Core.Handlers;

namespace Xpenses.API;

public static class BuilderConfiguration
{
    public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        /* Database Configuration Service */
        var connection = builder.Configuration.GetConnectionString("Default");
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));

        /* Identity Configuration Service */
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
        
        /* Swagger config */
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( options => options.CustomSchemaIds(n => n.FullName));
        
        /* DI Config */
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
        
        /* Auth/Authorization Config */
        services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();

    }
}