using Xpenses.API.Common.Api;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

namespace Xpenses.API.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}",HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .Produces<Response<Category?>>();
    
    
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.UserId = "123";
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);
        
        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
        
        
    }
}