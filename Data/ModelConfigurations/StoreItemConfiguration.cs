using Domain.StoreItems;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfigurations;

internal class StoreItemConfiguration : IEntityTypeConfiguration<StoreItem>
{
    public void Configure(EntityTypeBuilder<StoreItem> builder)
    {
        builder
            .HasIndex(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
           .Property(x => x.Name)
           .IsRequired()
           .HasMaxLength(128);

        builder
           .Property(x => x.Description)
           .HasMaxLength(4000);

        builder
           .Property(x => x.Cost)
           .IsRequired();

        builder
            .HasOne<User>(x => x.Seller)
            .WithMany(x => x.OfferedItems)
            .HasForeignKey(x => x.SellerId);
    }
}
