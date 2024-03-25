using Data;
using Data.Validators.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Tests.Unit;

public class UserValidationTests
{
    private readonly DbApplicationContext _dbContext;

    public UserValidationTests()
    {
        var contextOptions = new DbContextOptionsBuilder<DbApplicationContext>()
            .UseInMemoryDatabase("UsersValidationTest")
            .Options;
        _dbContext = new DbApplicationContext(contextOptions);
    }

    private const string USER_EMAIL = "test@gmail.com"; 
    private const string USER_PASSWORD = "Abc.123456789"; 
    private const string USER_NAME = "Test User"; 
    private const string USER_CONTACTS = "+7(800)800-80-80"; 

    [Theory]
    [InlineData(USER_EMAIL, UserValidationError.None)]
    [InlineData("t@gmail.com", UserValidationError.None)]
    [InlineData("", UserValidationError.EmailInvalidFormat)]
    [InlineData("тест@gmail.com", UserValidationError.EmailInvalidFormat)]
    [InlineData("test@мейл.ру", UserValidationError.EmailInvalidFormat)]
    [InlineData("test", UserValidationError.EmailInvalidFormat)]
    [InlineData("test.com", UserValidationError.EmailInvalidFormat)]
    [InlineData("test@gmailcom", UserValidationError.EmailInvalidFormat)]
    public async void TestEmailFormatValidation(string email, UserValidationError expectedResult)
    {
        var userValidator = CreateValidator(new List<User>());
        var result = await userValidator.ValidateUser(email, USER_PASSWORD, USER_NAME, USER_CONTACTS);
        Assert.Equal(result, expectedResult);
    }

    private readonly List<User> _predefinedUsers = new()
    {
        new User { Name = "A", Email = "test1@gmail.com", Password = Encoding.ASCII.GetBytes("123") },
        new User { Name = "B", Email = "test2@gmail.com", Password = Encoding.ASCII.GetBytes("321") },
        new User { Name = "C", Email = "test3@gmail.com", Password = Encoding.ASCII.GetBytes("111") },
    };

    [Theory]
    [InlineData("test0@gmail.com", UserValidationError.None)]
    [InlineData("test1@gmail.com", UserValidationError.EmailAlreadyExist)]
    [InlineData("test2@gmail.com", UserValidationError.EmailAlreadyExist)]
    public async void TestEmailOccupationValidation(string email, UserValidationError expectedResult)
    {
        var userValidator = CreateValidator(_predefinedUsers);
        var result = await userValidator.ValidateUser(email, USER_PASSWORD, USER_NAME, USER_CONTACTS);
        Assert.Equal(result, expectedResult);
    }

    private UserValidator CreateValidator(List<User> predefinedUsers)
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        _dbContext.Set<User>().AddRange(predefinedUsers);
        _dbContext.SaveChanges();

        return new UserValidator(_dbContext);
    }
}