using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.WebApp.ActionFilters;
using GeradorDeTestes.WebApp.DependencyInjection;
using GeradorDeTestes.WebApp.Orm;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
using QuestPDF.Infrastructure;
using GeradorDeTestes.Aplicacao;
using GeradorDeTestes.Aplicacao.ModuloMateria;
using GeradorDeTestes.Aplicacao.ModuloQuestao;
using GeradorDeTestes.Aplicacao.ModuloTeste;

namespace GeradorDeTestes.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            QuestPDF.Settings.License = LicenseType.Community;
  
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<LogarAcaoAttribute>();
            });

            builder.Services.AddScoped<DisciplinaAppService>();
            builder.Services.AddScoped<MateriaAppService>();
            builder.Services.AddScoped<QuestaoAppService>();
            builder.Services.AddScoped<TesteAppService>();
            builder.Services.AddScoped<IRepositorioMateria, RepositorioMateriaEmOrm>();
            builder.Services.AddScoped<IRepositorioDisciplina, RepositorioDisciplinaEmOrm>();
            builder.Services.AddScoped<IRepositorioTeste, RepositorioTesteEmOrm>();
            builder.Services.AddScoped<IRepositorioQuestao, RepositorioQuestaoEmOrm>();

            builder.Services.AddQuestPDFConfig();
            builder.Services.AddEntityFrameworkConfig(builder.Configuration);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/erro");
            else
                app.UseDeveloperExceptionPage();

            app.ApplyMigrations();

            app.UseAntiforgery();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
