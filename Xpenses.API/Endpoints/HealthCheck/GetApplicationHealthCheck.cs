using Xpenses.API.Common.Api;

namespace Xpenses.API.Endpoints.HealthCheck;

public class GetApplicationHealthCheck : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
   => app.MapGet("/", () => "Ok")
       .WithName("HealthCheck")
       .WithSummary("Retorna status da aplicação");
}