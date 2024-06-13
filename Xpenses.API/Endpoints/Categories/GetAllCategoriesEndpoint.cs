using Xpenses.API.Common.Api;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

namespace Xpenses.API.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
        .WithName("Categories: Pegar todas as categorias de determinado usuário")
        .WithSummary("Retorna todas as categorias de um determinado usuario")
        .Produces<PagedResponse<List<Category?>>>();
    
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "123"
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result?.Data)
            : TypedResults.BadRequest(result?.Data);
    }
}