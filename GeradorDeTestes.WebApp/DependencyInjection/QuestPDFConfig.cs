using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Pdf;

namespace GeradorDeTestes.WebApp.DependencyInjection;

public static class QuestPDFConfig
{
    public static void AddQuestPDFConfig(this IServiceCollection services)
    {
        services.AddScoped<IGeradorTeste, GeradorTesteEmPdf>();
    }
}
