using Kusanagi.Domain.Repositories;

namespace Kusanagi.Infrastructure.DataAcess;

public class UnitOfWork : IUnitOfWork
{
    private readonly KusanagiDbContext _context;

    public UnitOfWork(KusanagiDbContext context)
    {
        _context = context;
    }

    public async Task Commit()=> await _context.SaveChangesAsync();
}
