using Microsoft.EntityFrameworkCore;
using Xpenses.API.Data;
using Xpenses.Core.Entities;
using Xpenses.Core.Handlers;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

namespace Xpenses.API.Handlers;

public class CategoryHandler(AppDbContext ctx) : ICategoryHandler
{

    private readonly string errorFlag = "[CategoryHandler]";
    
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                Description = request.Description,
                Title = request.Title,
                UserId = request.UserId
            };
            await ctx.Categories.AddAsync(category);
            await ctx.SaveChangesAsync();

            return new Response<Category>(category, 201, $"Categoria {category.Id} criada.");
        }
        catch (Exception ex)
        {
            return new Response<Category?>(null, 500, $"{errorFlag} {ex.Message}");
        }
    }
    
    
    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await ctx.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null)
                return new Response<Category>(null, 404, "Categoria não encontrada.");
            
            category.Title = request.Title;
            category.Description = request.Description;
            category.UserId = request.UserId;
            
            ctx.Categories.Update(category);
            await ctx.SaveChangesAsync();

            return new Response<Category?>(category);
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, $"{errorFlag} {e.Message}");
        }
        
    }
   
    
    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        { 
            var category = await ctx.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (category is null)
            return new Response<Category>(null, 404, "Categoria não encontrada.");

        ctx.Categories.Remove(category);
        await ctx.SaveChangesAsync();

        return new Response<Category?>(category, 200, $"Categoria ${category.Id} removida.");

        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, $"{errorFlag} {e.Message}");
        }
    }
   
    
    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var id = request.Id;
            var userId = request.UserId;
            
            var category = await ctx.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            
           return category is null 
               ? new Response<Category?>(null, 404, "Categoria não encontrada")
               : new Response<Category?>(category);
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, $"{errorFlag} {e.Message}");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = ctx
                .Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);
            
            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize) 
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            
            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Category>>(null, 500, $"{errorFlag} {e.Message}");
        }
       
        
    }

    
    
}