using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xpenses.Core.Entities;

namespace Xpenses.API.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> b)
    {
        b.ToTable("Transaction");
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Title)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        b.Property(x => x.Type)
            .IsRequired(true)
            .HasColumnType("SMALLINT");

        b.Property(x => x.Amount)
            .IsRequired(true)
            .HasColumnType("MONEY")
            .HasMaxLength(255);
        
        
        b.Property(x => x.CreatedAt)
            .IsRequired(true);

        b.Property(x => x.PaidOrReceivedAt)
            .IsRequired(false);
        
        b.Property(x => x.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

    }
}