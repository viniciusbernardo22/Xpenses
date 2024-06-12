using Xpenses.Core.Entities;
using Xpenses.Core.Requests.Categories;
using Xpenses.Core.Responses;

namespace Xpenses.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
}