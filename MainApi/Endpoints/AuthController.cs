using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Users;
using MainApi.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Data;
using Microsoft.Extensions.Options;
using Common.Encryption;

namespace MainApi.Endpoints;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly IEncryptionService _encryptionService;
    private readonly AuthOptions _authOptions;
    private readonly DbApplicationContext _dbContext;

    public AuthController(
        IOptions<AuthOptions> authOptions,
        IEncryptionService encryptionService,
        DbApplicationContext dbContext)
    {
        _authOptions = authOptions.Value;
        _encryptionService = encryptionService;
        _dbContext = dbContext;
    }


    [HttpPost("login")]
    public async Task<IResult> Login([FromForm] string email, [FromForm] string password)
    {
        User? person = _dbContext.Set<User>().FirstOrDefault(p => p.Email == email);
        if (person is null) 
        {
            return Results.Unauthorized();
        }

        var encryptedPassword = _encryptionService.Encrypt(password);
        if (person.Password.SequenceEqual(encryptedPassword) == false)
        {
            return Results.Unauthorized();
        }

        var claims = new List<Claim> { new (ClaimTypes.Name, email) };
        var jwt = new JwtSecurityToken(
                issuer: _authOptions.TokenIssuer,
                audience: _authOptions.TokenAudience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(_authOptions.TtlInSeconds)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
        };

        return Results.Json(response);
    }

    [HttpPost("logout")]
    public async Task Logout()
    {

    }
}
