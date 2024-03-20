#if DEBUG

using Microsoft.AspNetCore.Mvc;
using Domain.Users;
using Data;
using Domain.Purchases;
using Domain.StoreItems;

namespace MainApi.Endpoints;

[ApiController]
[Route("[controller]")]
public class TestingController
{
    private readonly DbApplicationContext _dbContext;

    public TestingController(
        DbApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("clear-db")]
    public async Task<IResult> ClearDatabase()
    {
        ClearSet<PurchasedItem>();
        ClearSet<Purchase>();
        ClearSet<StoreItem>();
        ClearSet<User>();
        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    private void ClearSet<T>() where T : class 
    {
        var set = _dbContext.Set<T>();
        set.RemoveRange(set.ToList());
    }
}

#endif
