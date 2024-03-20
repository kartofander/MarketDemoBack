using Data.Abstracts;
using Domain.StoreItems;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Queries;

public class StoreItemsQuery : DbQuery<StoreItem>
{
    public StoreItemsQuery(DbApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task<StoreItem[]> GetAllStoreItems(int page, int pageSize)
    {
        return await GetQueryableAsNoTracking()
            .Page(page, pageSize)
            .ToArrayAsync();
    }

    public async Task<StoreItem[]> GetAvailableStoreItems(int page, int pageSize, Expression<Func<StoreItem, IComparable>> orderSelector, bool descending)
    {
        return await GetQueryableAsNoTracking()
            .Where(x => x.Status == StoreItemStatus.Available)
            .OrderBy(orderSelector)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();
    }
}
