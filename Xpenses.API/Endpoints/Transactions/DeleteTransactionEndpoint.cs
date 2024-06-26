﻿using System.Security.Claims;
using Xpenses.API.Common.Api;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Transactions;
using Xpenses.Core.Responses;

namespace Xpenses.API.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Exclui uma transação")
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
    {
        var request = new DeleteTransactionRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
            
        var result = await handler.DeleteAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result.Data);
    }
}