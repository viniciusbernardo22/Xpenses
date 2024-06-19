using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Xpenses.API.Data.Mappings.Identity;

public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> b)
    {
        b.ToTable("IdentityUserToken");
        b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        b.Property(t => t.LoginProvider).HasMaxLength(120);
        b.Property(t => t.Name).HasMaxLength(180);
    }
}