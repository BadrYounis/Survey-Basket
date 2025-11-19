using Microsoft.AspNetCore.Identity;
using SurveyBasket.Authentication;

namespace SurveyBasket.Services;
public class AuthService(UserManager<ApplicationUser> _usermanager, IJwtProvider _jwtProvider) : IAuthService
{
    public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        //check user?
        var user = await _usermanager.FindByEmailAsync(email);

        if (user is null)
            return null;

        //check password
        var isValidPassword = await _usermanager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
            return null;

        //generate Jwt token inside IJwtProvider and JwtProvider
        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        //return new AuthResponse()
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
    }
}