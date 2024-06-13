using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xpenses.API.Data;
using Xpenses.API.Handlers;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => options.CustomSchemaIds(n => n.FullName));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Ok");

app.MapGet("/v1/categories", async ( ICategoryHandler handler) =>
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "123"
        };
        return await handler.GetAllAsync(request);
    }) 
    .WithName("Categories: Pegar todas as categorias de determinado usuário")
    .WithSummary("Retorna todas as categorias de um determinado usuario")
    .Produces<PagedResponse<List<Category>>>();

app.MapGet("/v1/categories/{id}", async (long id, ICategoryHandler handler) =>
    {
        var request = new GetCategoryByIdRequest
        {
            Id = id,
            UserId = "123"
        };
        return await handler.GetByIdAsync(request);
    }) 
    .WithName("Categories: Pegar pelo Id")
    .WithSummary("Procura uma categoria pelo Id")
    .Produces<Response<Category>>();

app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
    { 
        request.Id = id;
     return await handler.UpdateAsync(request);
})
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category>>();

app.MapDelete("/v1/categories/{id}", async (long id, ICategoryHandler handler) =>
    {
        var request = new DeleteCategoryRequest
        {
            Id = id
        };
       return await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .Produces<Response<Category>>();


app.Run();

