using MainApi.Dtos;

namespace MainApi.Services.Purchases;

public interface IPurchaseService
{
    public Task<long> Purchase(PurchaseRequestDto purchaseDto, string? buyer = null);
}
