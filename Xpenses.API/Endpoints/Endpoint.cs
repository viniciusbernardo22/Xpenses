using Xpenses.API.Common.Api;
using Xpenses.API.Endpoints.Categories;
using Xpenses.API.Endpoints.HealthCheck;
using Xpenses.API.Endpoints.Identity;
using Xpenses.API.Endpoints.Transactions;
using Xpenses.API.Models;

namespace Xpenses.API.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app;

        endpoints.MapGroup("/")
            .WithTags("HealthCheck")
                .MapEndpoint<GetApplicationHealthCheck>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .RequireAuthorization()
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetUserRolesEndpoint>()
            .MapIdentityApi<User>();
           

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>();
                

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .RequireAuthorization()
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