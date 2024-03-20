using Microsoft.AspNetCore.Mvc;
using MainApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Data.Queries;
using Data.Commands.Users;

namespace MainApi.Endpoints;

[ApiController]
[Route("[controller]")]
public class UsersController
{
    private readonly CreateUserCommand _createUserCommand;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UsersQuery _usersQuery;

    public UsersController(
        IHttpContextAccessor httpContextAccessor,
        UsersQuery usersQuery,
        CreateUserCommand createUserCommand) 
    {
        _httpContextAccessor = httpContextAccessor;
        _usersQuery = usersQuery;
        _createUserCommand = createUserCommand;
    }

    [HttpPost]
    public async Task<IResult> Register([FromBody] UserDto userDto)
    {
        var validationResult = await _createUserCommand.Execute(
            userDto.Email,
            userDto.Password,
            userDto.Name,
            userDto.Contacts);

        return Results.Ok(validationResult);
    }

    [HttpPut("name")]
    [Authorize]
    public async Task<IResult> UpdateUserName([FromForm] string name)
    {

        return Results.Ok();
    }

    [HttpPut("contacts")]
    [Authorize]
    public async Task<IResult> UpdateUserContacts([FromForm] string contacts)
    {


        return Results.Ok();
    }
}
