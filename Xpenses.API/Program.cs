using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Xpenses.API.Data;
using Xpenses.Core.Entities;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => options.CustomSchemaIds(n => n.FullName));
builder.Services.AddTransient<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

string version = "v1";

app.MapGet("/", () => "Ok");

app.MapPost($"/{version}/categories",
        (Request request, Handler handler) => handler.Handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response>();

app.Run();

public class Request
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
}

public class Response
{
    public long Id { get; set; }
    public string Title { get; set; } = String.Empty;
}


public class Handler(AppDbContext ctx)
{
    public Response Handle(Request request)
    {
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description
        };
        
        ctx.Categories.Add(category);
        ctx.SaveChanges();
        
        return new Response
        {
            Id = category.Id,
            Title = category.Title
        };
    } 
}

