using Domain.Purchases;
using Domain.Users;

namespace MainApi.Dtos;

public class PurchaseRequestDto
{
    public PurchasedItemDto[] Items { get; set; }

    public PurchaseRequestDto()
    {
    }
}