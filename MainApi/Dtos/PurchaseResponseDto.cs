using Domain.Purchases;

namespace MainApi.Dtos;

public class PurchaseResponseDto
{
    public long? Id { get; set; }
    public long? PurchaserId { get; set; }
    public float? TotalCost { get; set; }
    public DateTime? CheckoutTime { get; set; }
    public PurchaseStatus? Status { get; set; }
    public PurchasedItemDto[] Items { get; set; }

    public PurchaseResponseDto()
    {
    }

    public PurchaseResponseDto(Purchase purchase)
    {
        Id = purchase.Id;
        PurchaserId = purchase.PurchaserId;
        TotalCost = purchase.TotalCost;
        Status = purchase.Status;
        CheckoutTime = purchase.CheckoutTime;
        Status = purchase.Status;
        TotalCost = purchase.TotalCost;
        Items = purchase.Items
            .Select(x => new PurchasedItemDto(x))
            .ToArray();
    } 
}