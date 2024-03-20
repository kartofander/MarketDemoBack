using Domain.Purchases;

namespace MainApi.Dtos;

public class PurchasedItemDto
{
    public long ItemId { get; set; }
    public int Count { get; set; }

    public PurchasedItemDto()
    {
    }

    public PurchasedItemDto(PurchasedItem item)
    {
        ItemId = item.ItemId;
        Count = item.Count;
    }
}
