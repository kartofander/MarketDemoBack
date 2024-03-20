using Data.Abstracts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Data.Validators.Users;

public class UserValidator : IValidator
{
    private const string EMAIL_REGEX = "[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}";

    private readonly DbApplicationContext _dbContext;

    public UserValidator(DbApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserValidationError> ValidateUser(
        string email,
        string password,
        string name,
        string contacts)
    {
        var validation = UserValidationError.None;

        validation |= await ValidateUserEmail(email);
        validation |= await ValidateUserName(name);
        validation |= await ValidateUserPassword(password);
        validation |= await ValidateUserContacts(contacts);

        return validation;
    }

    public async Task<UserValidationError> ValidateUserEmail(string email)
    {
        var match = Regex.Match(email, EMAIL_REGEX, RegexOptions.IgnoreCase);

        if (match.Success == false)
        {
            return UserValidationError.EmailInvalidFormat;
        }

        var emailOccupied = await _dbContext.Set<User>().AnyAsync(x => x.Email == email);
        if (emailOccupied)
        {
            return UserValidationError.EmailAlreadyExist;
        }

        return UserValidationError.None;
    }

    private async Task<UserValidationError> ValidateUserName(string name)
    {
        // ToDo: implement validation
        return await Task.FromResult(UserValidationError.None);
    }

    private async Task<UserValidationError> ValidateUserPassword(string password)
    {
        // ToDo: implement validation
        return await Task.FromResult(UserValidationError.None);
    }

    private async Task<UserValidationError> ValidateUserContacts(string password)
    {
        // ToDo: implement validation
        return await Task.FromResult(UserValidationError.None);
    }
}
