using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Xpenses.API.Common.Api;
using Xpenses.API.Data;
using Xpenses.API.Models;

namespace Xpenses.API.Endpoints.Identity;

public class GetUserRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/roles", HandleAsync)
            .WithName("Identity: Get User Roles")
            .WithSummary("Busca roles do usuario")
            .RequireAuthorization();
    
    
    private static IResult HandleAsync(ClaimsPrincipal userInfo)
    {
        if (userInfo.Identity is null || !userInfo.Identity.IsAuthenticated)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)userInfo.Identity;
        
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c =>
            new {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });
        return TypedResults.Json(roles);

    }
}