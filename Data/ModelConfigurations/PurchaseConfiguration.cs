using Domain.Purchases;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;

internal class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder
            .HasIndex(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .Property(x => x.TotalCost)
            .IsRequired();

        builder
            .Property(x => x.Status)
            .IsRequired();

        builder
            .Property(x => x.CheckoutTime)
            .IsRequired();

        builder
            .HasOne<User>(x => x.Purchaser)
            .WithMany(x => x.Purchases)
            .HasForeignKey(x => x.PurchaserId);
    }
}
