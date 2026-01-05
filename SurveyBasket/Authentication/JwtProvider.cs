using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SurveyBasket.Authentication;
public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    public (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
    {
        //[1] Add Claims That Are Found Inside Token
        Claim[] claims = [
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName, user.LastName),
            new (JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString()),
            new (nameof(roles), JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray),
            new (nameof(permissions), JsonSerializer.Serialize(permissions), JsonClaimValueTypes.JsonArray)
        ];

        //[2] Generate Key That Are Used To Encode and Decode Token
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        //[3] JWT should be cryptographically signed, and with which algorithm and key.
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        //[4] Generate Token
        var token = new JwtSecurityToken(
             issuer: _jwtOptions.Issuer,
             audience: _jwtOptions.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
             signingCredentials: signingCredentials
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _jwtOptions.ExpiryMinutes * 60);
    }
    public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch
        {
            return null;
        }
    }
}