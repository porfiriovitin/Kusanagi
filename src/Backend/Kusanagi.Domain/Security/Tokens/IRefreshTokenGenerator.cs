namespace Kusanagi.Domain.Security.Tokens;

public interface IRefreshTokenGenerator
{
    string Generate();
}
