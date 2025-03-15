using EasyLoginBase.Domain.Entities.User;
using EasyLoginBase.InfrastructureData.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;

namespace EasyLoginBase.CrossCutting.DependencyInjection.Extensions;

public static class IdentityConfiguration
{
    public static void Configurar(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        var serverVersionAutoDetec = ServerVersion.AutoDetect(connectionString);
        
        serviceCollection.
        AddDbContext<MyContext>(options =>
                     options.UseMySql(connectionString, serverVersionAutoDetec));


        serviceCollection.AddDefaultIdentity<UserEntity>()
            .AddRoles<RoleEntity>()
            .AddEntityFrameworkStores<MyContext>()
            .AddDefaultTokenProviders();

        serviceCollection.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        });

        serviceCollection.AddTransient<Func<IDbConnection>>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return () => new MySqlConnection(connectionString);
        });
    }
}
