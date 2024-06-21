using Microsoft.AspNetCore.Identity;
using Xpenses.API.Common.Api;
using Xpenses.API.Models;

namespace Xpenses.API.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/logout", HandleAsync)
            .WithName("Identity: Logout")
            .WithSummary("Realiza Logout");

    private static async Task<IResult> HandleAsync(SignInManager<User> manager)
    {
        await manager.SignOutAsync();
        return Results.Ok();
    }
}