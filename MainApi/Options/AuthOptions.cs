using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MainApi.Options;

public class AuthOptions
{
    public string SigningKey { get; set; }
    public string TokenIssuer { get; set; }
    public string TokenAudience { get; set; }
    public int TtlInSeconds { get; set; }
    public SymmetricSecurityKey GetSymmetricSecurityKey() 
        => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey));
}
