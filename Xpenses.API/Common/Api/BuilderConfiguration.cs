using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Xpenses.API.Data;
using Xpenses.API.Handlers;
using Xpenses.API.Models;
using Xpenses.Core;
using Xpenses.Core.Handlers;

namespace Xpenses.API.Common.Api;

public static class BuilderConfiguration
{
    public static void ApplyBuilderConfigurations(this WebApplicationBuilder builder)
    {
        builder.AddConfiguration();
        builder.AddSecurity();
        builder.AddDataContexts();
        builder.AddCrossOrigin();
        builder.AddDocumentation();
        builder.AddServices();
        builder.AddOpenTelemetry();
    }

    private static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("Default") ?? string.Empty;;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl")?? string.Empty;;
        Configuration.FrontEndUrl = builder.Configuration.GetValue<string>("FrontendUrl")?? string.Empty;;
        Configuration.CorsPolicyName = builder.Configuration.GetValue<string>("PolicyName") ?? string.Empty;
    }
     
    
    private static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(n => n.FullName));
    }

    private static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();
    }
    
    private static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Configuration.ConnectionString));
        
        builder.Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    } 
    
    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
    
    private static void AddOpenTelemetry(this WebApplicationBuilder builder)
    {
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
    }

    private static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                    Configuration.CorsPolicyName,
                    policy => policy
                        .WithOrigins(Configuration.BackendUrl, Configuration.FrontEndUrl)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
        ));
    }

    
    
}