using Xpenses.API.Common.Api;
using Xpenses.API.Models;

namespace Xpenses.API.Endpoints.HealthCheck;

public class GetApplicationHealthCheck : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
   => app.MapGet("/", () => new ServerInfoStatus())
       .WithName("HealthCheck")
       .WithSummary("Retorna status da aplicação");
}