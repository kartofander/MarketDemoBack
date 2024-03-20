using Domain.StoreItems;

namespace Domain.Purchases;

public class PurchasedItem
{
    public Purchase Purchase { get; set; }
    public long PurchaseId { get; set; }
    public StoreItem Item { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
    public float Cost { get; set; }
    public PurchaseStatus Status { get; set; }
}
