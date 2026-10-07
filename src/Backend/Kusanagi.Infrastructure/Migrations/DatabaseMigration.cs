using Kusanagi.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kusanagi.Infrastructure.Migrations;

public class DatabaseMigration
{
    public static async Task ExecuteMigrations(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<KusanagiDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
