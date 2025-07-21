using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.WebApp.ActionFilters;
using GeradorDeTestes.WebApp.DependencyInjection;
using GeradorDeTestes.WebApp.Orm;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
using GeradorDeTestes.WebApp.Services;
using QuestPDF.Infrastructure;

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


            builder.Services.AddScoped<IRepositorioMateria, RepositorioMateriaEmOrm>();
            builder.Services.AddScoped<IRepositorioDisciplina, RepositorioDisciplinaEmOrm>();
            builder.Services.AddScoped<IRepositorioTeste, RepositorioTesteEmOrm>();

            // builder.Services.AddScoped<IRepositorioDeVoces, RepositorioDeVoces>();
            
            builder.Services.AddScoped<IRepositorioQuestao, RepositorioQuestaoEmOrm>();

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
