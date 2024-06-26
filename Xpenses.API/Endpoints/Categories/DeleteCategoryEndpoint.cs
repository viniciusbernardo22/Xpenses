﻿using System.Security.Claims;
using Xpenses.API.Common.Api;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

namespace Xpenses.API.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
        .WithName("Categories: Delete")
        .WithSummary("Exclui uma categoria")
        .Produces<Response<Category?>>();
    
    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}