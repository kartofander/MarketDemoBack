using Domain.Purchases;
using Domain.StoreItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;

internal class PurchasedItemConfiguration : IEntityTypeConfiguration<PurchasedItem>
{
    public void Configure(EntityTypeBuilder<PurchasedItem> builder)
    {
        builder
            .HasKey(x => new { x.PurchaseId, x.ItemId });

        builder
            .HasIndex(x => new {x.PurchaseId, x.ItemId});

        builder
            .Property(x => x.Cost)
            .IsRequired();

        builder
            .Property(x => x.Count)
            .IsRequired();

        builder
            .HasOne<Purchase>(x => x.Purchase)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.PurchaseId);

        builder
            .HasOne<StoreItem>(x => x.Item)
            .WithMany()
            .HasForeignKey(x => x.ItemId);
    }
}
