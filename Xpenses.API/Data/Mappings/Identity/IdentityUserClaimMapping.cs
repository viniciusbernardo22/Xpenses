using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Xpenses.API.Data.Mappings.Identity;

public class IdentityUserClaimMapping
    : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> b)
    {
        b.ToTable("IdentityUserClaim");
        b.HasKey(u => u.Id);
        b.Property(u => u.ClaimType).HasMaxLength(255);
        b.Property(u => u.ClaimValue).HasMaxLength(255);
    }
}