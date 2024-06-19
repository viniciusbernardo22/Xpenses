using Microsoft.AspNetCore.Identity;

namespace Xpenses.API.Models;

public class ApplicationUser : IdentityUser<long>
{
    // RBAC 
    public List<IdentityRole<long>>? Roles { get; set; }
}