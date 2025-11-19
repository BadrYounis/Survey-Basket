using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Authentication;
public class JwtProvider : IJwtProvider
{
    public (string token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        //[1] Add Claims That Are Found Inside Token
        Claim[] claims = [
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName, user.LastName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        //[2] Generate Key That Are Used To Encode and Decode Token
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("m13VHAFTfi2MxEMqZVPjqsUqqvfSmHY4"));

        //[3] JWT should be cryptographically signed, and with which algorithm and key.
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expiresIn = 30;

        //[4] Generate Token
        var token = new JwtSecurityToken(
             issuer: "SurveyBasketApp",
             audience: "SurveyBasketApp Users",
             claims: claims,
             expires: DateTime.UtcNow.AddMinutes(expiresIn),
             signingCredentials: signingCredentials          
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresIn: expiresIn * 60);
    }
}