using Kusanagi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace Kusanagi.Infrastructure.DataAcess;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options){ }

    public DbSet<User> Users { get; set; }

}
