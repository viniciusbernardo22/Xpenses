namespace Xpenses.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    public long Id { get; set; }
    public string Title { get; set; }= string.Empty;
    public string Description { get; set; } = string.Empty;
}