using Kusanagi.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DbContext = Kusanagi.Infrastructure.DataAcess.DbContext;

namespace Kusanagi.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            options.UseNpgsql(connectionString);
        });
    }


}
