namespace Kusanagi.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
