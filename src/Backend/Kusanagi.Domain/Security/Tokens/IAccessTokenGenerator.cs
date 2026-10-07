using Kusanagi.Domain.Entities;

namespace Kusanagi.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
