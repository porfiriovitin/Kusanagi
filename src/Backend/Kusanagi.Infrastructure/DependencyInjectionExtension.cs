using Kusanagi.Domain.Repositories;
using Kusanagi.Domain.Repositories.User;
using Kusanagi.Domain.Security.PasswordHasher;
using Kusanagi.Domain.Security.Tokens;
using Kusanagi.Infrastructure.DataAccess;
using Kusanagi.Infrastructure.DataAccess.Repositories;
using Kusanagi.Infrastructure.Security.PasswordHashing;
using Kusanagi.Infrastructure.Security.Tokens;
using Kusanagi.Infrastructure.Security.Tokens.Access;
using Kusanagi.Infrastructure.Security.Tokens.Refresh;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kusanagi.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddSecurityHandler(services);
        AddTokensHandler(services, configuration);
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KusanagiDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            options.UseNpgsql(connectionString);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
    }

    private static void AddSecurityHandler(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
    }

    private static void AddTokensHandler(this IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeInMinutes = configuration.GetValue<uint>("Jwt:ExpirationTimeInMinutes");
        var SigningKey = configuration.GetValue<string>("Jwt:SigningKey")!;

        services.AddScoped<IAccessTokenGenerator>(provider => new JwtTokenHandler(expirationTimeInMinutes, SigningKey));
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenHandler>();

    }

}
