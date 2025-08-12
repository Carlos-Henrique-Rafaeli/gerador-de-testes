using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Testes.Integracao.Compartilhado;

public class GeradorDeTestesDbContextFactory
{
    public static GeradorDeTestesDbContext CriarDbContext(string connectionString)
    {
        var options = new DbContextOptionsBuilder<GeradorDeTestesDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        var dbContext = new GeradorDeTestesDbContext(options);

        return dbContext;
    }
}
