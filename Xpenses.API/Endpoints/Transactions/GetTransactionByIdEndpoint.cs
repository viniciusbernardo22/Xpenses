﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Xpenses.API.Common.Api;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Transactions;
using Xpenses.Core.Responses;

namespace Xpenses.API.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) 
        => app.MapGet("/{id}", HandleAsync)
        .WithName("Transactions: Get By Id")
        .WithSummary("Busca uma transação pelo Id")
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, [FromQuery] long id)
    {
        var request = new GetTransactionByIdRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var response = await handler.GetByIdAsync(request);

        return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response.Data);
    }
}