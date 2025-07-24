using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.WebApp.DependencyInjection;

public static class EntityFrameworkConfig
{
    public static void AddEntityFrameworkConfig(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        var connectionString = configuration["SQL_CONNECTION_STRING"];

        services.AddDbContext<IUnitOfWork, GeradorDeTestesDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}
