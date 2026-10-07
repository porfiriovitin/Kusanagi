using Kusanagi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace Kusanagi.Infrastructure.DataAccess;

public class KusanagiDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public KusanagiDbContext(DbContextOptions<KusanagiDbContext> options) : base(options){ }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique()
            .HasFilter("\"Active\" = true");
    }

}
