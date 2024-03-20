using Common.Encryption;
using Data.Abstracts;
using Data.Validators;
using Data.Validators.Users;
using Domain.StoreItems;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Data.Commands.StoreItems;

public class CreateStoreItemCommand : DbCommand<StoreItem>
{
    private readonly StoreItemValidator _storeItemValidator;

    public CreateStoreItemCommand(
        StoreItemValidator storeItemValidator,
        DbApplicationContext dbContext)
        : base(dbContext)
    {
        _storeItemValidator = storeItemValidator;
    }

    public async Task<(long id, StoreItemValidationError validation)> Execute(
        string sellerEmail,
        string name,
        float cost,
        string description)
    {
        var validation = await _storeItemValidator.Validate(name, cost, description);
        if (validation != StoreItemValidationError.None)
        {
            return (-1, validation);
        }

        var seller = await _dbContext
            .Set<User>()
            .SingleAsync(x => x.Email == sellerEmail);

        var now = DateTime.UtcNow;

        var newStoreItem = new StoreItem
        {
            Name = name,
            Description = description,
            Cost = cost,
            Seller = seller,
            SellerId = seller.Id,
            Created = now,
            LastUpdated = now,
            Status = StoreItemStatus.Available,
        };

        await _dbContext.Set<StoreItem>().AddAsync(newStoreItem);
        await _dbContext.SaveChangesAsync();

        return (newStoreItem.Id, validation);
    }
}
