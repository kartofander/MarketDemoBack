using Common.Encryption;
using Data.Abstracts;
using Data.Validators.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Data.Commands.Users;

public class CreateUserCommand : DbCommand<User>
{
    private readonly IEncryptionService _encyptionService;
    private readonly UserValidator _userValidator;

    public CreateUserCommand(
        IEncryptionService encyptionService,
        UserValidator userValidator, 
        DbApplicationContext dbContext) 
        : base(dbContext)
    {
        _encyptionService = encyptionService;
        _userValidator = userValidator;
    }

    public async Task<UserValidationError> Execute(
        string email, 
        string password, 
        string name, 
        string contacts)
    {
        var validation = await _userValidator.ValidateUser(email, password, name, contacts);
        if (validation != UserValidationError.None)
        {
            return validation;
        }

        await _dbContext
            .Set<User>()
            .AddAsync(new User
            {
                Email = email,
                Password = _encyptionService.Encrypt(password),
                Name = name,
                Contacts = contacts,
            });

        await _dbContext.SaveChangesAsync();

        return validation;
    }
}
