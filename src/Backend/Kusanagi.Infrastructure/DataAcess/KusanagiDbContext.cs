using Kusanagi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace Kusanagi.Infrastructure.DataAcess;

public class KusanagiDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public KusanagiDbContext(DbContextOptions<KusanagiDbContext> options) : base(options){ }

    public DbSet<User> Users { get; set; }

}
