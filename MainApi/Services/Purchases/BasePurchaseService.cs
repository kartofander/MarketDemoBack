using Data;
using Domain.Purchases;
using Domain.StoreItems;
using Domain.Users;
using MainApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Services.Purchases;

public abstract class BasePurchaseService : IPurchaseService
{
    private readonly DbApplicationContext _dbContext;

    public BasePurchaseService(DbApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public abstract Task NotifySellers(long purchaseId);

    public async Task<long> Purchase(PurchaseRequestDto purchaseDto, string? buyer = null)
    {
        var purchaseId = await SavePurchase(purchaseDto, buyer);
        await NotifySellers(purchaseId);
        return purchaseId;
    }

    private async Task<long> SavePurchase(PurchaseRequestDto purchaseDto, string? buyer = null)
    {
        var purchase = new Purchase()
        {
            CheckoutTime = DateTime.UtcNow,
            Status = PurchaseStatus.AwaitingPayment,
            Items = new List<PurchasedItem>(),
        };

        await _dbContext.Set<Purchase>().AddAsync(purchase);


        if (buyer != null)
        {
            purchase.Purchaser = await _dbContext.Set<User>().SingleAsync(x => x.Email == buyer);
        }

        foreach (var itemDto in purchaseDto.Items)
        {
            var item = await _dbContext.Set<StoreItem>().SingleAsync(x => x.Id == itemDto.ItemId);

            var purchItem = new PurchasedItem()
            {
                Purchase = purchase,
                Item = item,
                Count = itemDto.Count,
                Cost = item.Cost,
                Status = PurchaseStatus.AwaitingPayment,
            };

            await _dbContext.Set<PurchasedItem>().AddAsync(purchItem);
        }

        await _dbContext.SaveChangesAsync();
        return purchase.Id;
    }
}