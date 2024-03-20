
using Data.Abstracts;

namespace Data.Validators;

public class StoreItemValidator : IValidator
{
    private readonly DbApplicationContext _dbContext;

    public StoreItemValidator(DbApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StoreItemValidationError> Validate(
        string name,
        float cost,
        string description)
    {
        var validation = StoreItemValidationError.None;

        return validation;
    }
}
