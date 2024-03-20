using Data;

namespace MainApi.Services.Purchases;

public class DummyPurchaseService : BasePurchaseService
{
    public DummyPurchaseService(DbApplicationContext dbContext) : base(dbContext)
    {
    }

    public override Task NotifySellers(long purchaseId)
    {
        return Task.CompletedTask;
    }
}