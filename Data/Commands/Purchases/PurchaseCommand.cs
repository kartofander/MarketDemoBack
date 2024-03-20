using Data.Abstracts;
using Domain.StoreItems;
using Microsoft.EntityFrameworkCore;

namespace Data.Commands.Purchases;

public class PurchaseCommand : DbCommand<StoreItem>
{
    public PurchaseCommand(DbApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateStoreItem(StoreItem storeItem)
    {
        await _dbContext.Set<StoreItem>().AddAsync(storeItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateStoreItem(StoreItem storeItem)
    {
        var itemToUpdate = GetQueryable()
            .SingleOrDefault(x => x.Id == storeItem.Id);

        if (itemToUpdate == null)
        {
            return;
        }

        itemToUpdate.Name = storeItem.Name;
        itemToUpdate.Description = storeItem.Description;
        itemToUpdate.Cost = storeItem.Cost;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteStoreItem(long itemId)
    {
        var itemToRemove = await GetQueryable()
            .SingleOrDefaultAsync(x => x.Id == itemId);

        if (itemToRemove != null)
        {
            _dbContext.Set<StoreItem>()
                .Remove(itemToRemove);
        }

        await _dbContext.SaveChangesAsync();
    }
}
