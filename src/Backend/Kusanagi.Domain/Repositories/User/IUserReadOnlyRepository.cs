namespace Kusanagi.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithId(Guid id);
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmail(string email);
}
