using Data.Abstracts;
using Data.Validators;
using Domain.StoreItems;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Data.Commands.StoreItems;

public class UpdateStoreItemCommand : DbCommand<StoreItem>
{
    private readonly StoreItemValidator _storeItemValidator;

    public UpdateStoreItemCommand(
        StoreItemValidator storeItemValidator,
        DbApplicationContext dbContext)
        : base(dbContext)
    {
        _storeItemValidator = storeItemValidator;
    }

    public async Task<StoreItemValidationError> Execute(
        string invokerEmail,
        long itemId,
        string name,
        float cost,
        string description)
    {
        var invoker = await _dbContext
            .Set<User>()
            .SingleAsync(x => x.Email == invokerEmail);

        var itemToUpdate = GetQueryable()
            .Single(x => x.Id == itemId);

        if (itemToUpdate.SellerId != invoker.Id)
        {
            throw new InvalidOperationException("User tried to change someone else's store item.");
        }

        var validation = await _storeItemValidator.Validate(name, cost, description);
        if (validation != StoreItemValidationError.None)
        {
            return validation;
        }

        itemToUpdate.Name = name;
        itemToUpdate.Description = description;
        itemToUpdate.Cost = cost;

        await _dbContext.SaveChangesAsync();

        return validation;
    }
}
