using Microsoft.AspNetCore.Identity;
using SurveyBasket.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.Services;
public class AuthService(UserManager<ApplicationUser> _usermanager, IJwtProvider _jwtProvider) : IAuthService
{
    private readonly int _refreshTokenExpiryDays = 14;
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

        //generate Jwt token inside IJwtProvider and JwtProvider & Refresh Token 
        var (token, expiresIn) = _jwtProvider.GenerateToken(user);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        // Save Into Database
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _usermanager.UpdateAsync(user);

        //return new AuthResponse()
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);
    }
    public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);
        if (userId is null)
            return null;

        var user = await _usermanager.FindByIdAsync(userId);
        if (user is null)
            return null;

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (userRefreshToken is null)
            return null;

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        // Save Into Database
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _usermanager.UpdateAsync(user);

        //return new AuthResponse()
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);
    }
    public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);
        if (userId is null)
            return false;

        var user = await _usermanager.FindByIdAsync(userId);
        if (user is null)
            return false;

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (userRefreshToken is null)
            return false;

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        await _usermanager.UpdateAsync(user);

        return true;
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}