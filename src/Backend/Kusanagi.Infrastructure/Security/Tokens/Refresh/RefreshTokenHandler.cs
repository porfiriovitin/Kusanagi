using Kusanagi.Domain.Security.Tokens;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace Kusanagi.Infrastructure.Security.Tokens.Refresh;

public class RefreshTokenHandler : IRefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);

        var token = WebEncoders.Base64UrlEncode(bytes);

        return HashRefreshToken(token);
    }


    private string HashRefreshToken(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);

        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToHexString(hashBytes);
    }
}
