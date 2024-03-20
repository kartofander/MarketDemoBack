using Data.Abstracts;
using Domain.StoreItems;
using Microsoft.EntityFrameworkCore;

namespace Data.Commands.StoreItems;

public class DeleteStoreItemCommand : DbCommand<StoreItem>
{
    public DeleteStoreItemCommand(
        DbApplicationContext dbContext)
        : base(dbContext)
    {
    }

    public async Task Execute(long itemId)
    {
        var itemToRemove = await GetQueryable()
            .SingleAsync(x => x.Id == itemId);

        itemToRemove.Status = StoreItemStatus.Deleted;

        await _dbContext.SaveChangesAsync();
    }
}
