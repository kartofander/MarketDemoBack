using Data.Abstracts;
using Domain.Purchases;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public class PurchasesQuery : DbQuery<Purchase>
{
    public PurchasesQuery(DbApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Purchase>> GetUserPurchases(string login, int page, int pageSize)
    {
        return await GetQueryableAsNoTracking()
            .Include(x => x.Purchaser)
            .Include(x => x.Items)
            .Where(x => x.Purchaser.Email == login)
            .OrderByDescending(x => x.CheckoutTime)
            .Page(page, pageSize)
            .ToListAsync();
    }

}
