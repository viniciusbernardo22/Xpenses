using Xpenses.API.Common.Api;
using Xpenses.API.Endpoints.Categories;
using Xpenses.API.Endpoints.Transactions;

namespace Xpenses.API.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app;
            
            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                    .MapEndpoint<CreateCategoryEndpoint>()
                    .MapEndpoint<GetAllCategoriesEndpoint>()
                    .MapEndpoint<GetCategoryByIdEndpoint>()
                    .MapEndpoint<UpdateCategoryEndpoint>()
                    .MapEndpoint<DeleteCategoryEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<GetTransactionByIdEndpoint>()
                .MapEndpoint<GetTransactionByPeriodEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>(); 
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        return app;
    }

    
    

}