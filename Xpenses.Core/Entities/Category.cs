
using Xpenses.Core.Entities.Base;

namespace Xpenses.Core.Entities;

public class Category : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
}