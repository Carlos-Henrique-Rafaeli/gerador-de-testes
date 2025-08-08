using Serilog;
using Serilog.Events;

namespace GeradorDeTestes.WebApp.DependencyInjection;

public static class SerilogConfig
{
    public static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        var licenseKey = configuration["NEWRELIC_LICENSE_KEY"];

        if (string.IsNullOrEmpty(licenseKey))
            throw new InvalidOperationException("A chave de licença da New Relic não foi encontrada nas variáveis de ambiente.");

        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "GeradorDeTestes", "error.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error)
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "Gerador-De-Testes-Db",
                licenseKey: licenseKey
            )
            .CreateLogger();

        logging.ClearProviders();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
    }
}