﻿using Microsoft.AspNetCore.Mvc;
using Xpenses.API.Common.Api;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

namespace Xpenses.API.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria");
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess 
            ? TypedResults.Created($"/{result?.Data?.Id}", result?.Data)
            : TypedResults.BadRequest(result?.Data);
    }
}