using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Xpenses.API.Data.Mappings.Identity;

public class IdentityUserLogin : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> b)
    {
        b.ToTable("IdentityUserLogin");
        b.HasKey(l => new
        {
            l.LoginProvider, l.ProviderKey
        });
        b.Property(l => l.LoginProvider).HasMaxLength(128);
        b.Property(l => l.ProviderKey).HasMaxLength(128);
        b.Property(l => l.ProviderDisplayName).HasMaxLength(255);
    }
}