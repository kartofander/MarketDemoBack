using Domain.Users;

namespace Domain.StoreItems;

public class StoreItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public float Cost { get; set; }

    public User Seller { get; set; }
    public long? SellerId { get; set; }

    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }

    public StoreItemStatus Status { get; set; }
}