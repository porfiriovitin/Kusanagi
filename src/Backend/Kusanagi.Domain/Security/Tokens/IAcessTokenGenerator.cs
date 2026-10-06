using Kusanagi.Domain.Entities;

namespace Kusanagi.Domain.Security.Tokens;

public interface IAcessTokenGenerator
{
    string Generate(User user);
}
