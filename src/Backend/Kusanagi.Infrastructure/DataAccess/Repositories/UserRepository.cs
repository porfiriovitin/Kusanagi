using Kusanagi.Domain.Entities;
using Kusanagi.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Kusanagi.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly KusanagiDbContext _dbContext;

    public UserRepository(KusanagiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user) => await _dbContext.AddAsync(user);

    public async Task<bool> ExistActiveUserWithId(Guid id) => await _dbContext.Users.AnyAsync(user => user.Id == id && user.Active);

    public async Task<bool> ExistsActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email == email && user.Active);

    public async Task<User?> GetByEmail(string email) => await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.Active);

    public async Task<User?> GetById(Guid id) => await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id && user.Active);

    public async Task UpdatePassword(Guid userId, string newPasswordHash) => await _dbContext.Users.Where(user => user.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(user => user.PasswordHash, newPasswordHash));
}
