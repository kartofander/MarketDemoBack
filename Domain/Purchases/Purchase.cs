using Domain.Users;

namespace Domain.Purchases;

public class Purchase
{
    public long Id { get; set; }
    public User Purchaser { get; set; }
    public long? PurchaserId { get; set; }
    public List<PurchasedItem> Items { get; set; }
    public float TotalCost { get; set; }
    public DateTime CheckoutTime { get; set; }
    public PurchaseStatus Status { get; set; }
}
