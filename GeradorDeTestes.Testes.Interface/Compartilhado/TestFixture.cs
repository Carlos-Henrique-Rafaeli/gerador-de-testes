using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Testes.Integracao.Compartilhado;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace GeradorDeTestes.Testes.Interface.Compartilhado;

[TestClass]
public abstract class TestFixture
{
    protected static IWebDriver? driver;
    protected static GeradorDeTestesDbContext? dbContext;

    protected static string enderecoBase = "https://localhost:7098";
    private static string connectionString = "Server=localhost;Port=5432;Database=GeradorDeTestesDb;UserId=postgres;Password=YourStrongPassword;";

    [TestInitialize]
    public void ConfigurarTestes()
    {
        dbContext = GeradorDeTestesDbContextFactory.CriarDbContext(connectionString);

        ConfigurarTabelas(dbContext);

        InicializarDriver();
    }

    [TestCleanup]
    public void FinalizarTestes()
    {
        FinalizarDriver();
    }

    private static void InicializarDriver()
    {
        var options = new FirefoxOptions();

        options.AddArgument("--start-maximized");

        driver = new FirefoxDriver();

        driver.Manage().Window.FullScreen();
    }

    private static void FinalizarDriver()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    private static void ConfigurarTabelas(GeradorDeTestesDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        dbContext.Testes.RemoveRange(dbContext.Testes);
        dbContext.Questoes.RemoveRange(dbContext.Questoes);
        dbContext.Materias.RemoveRange(dbContext.Materias);
        dbContext.Disciplinas.RemoveRange(dbContext.Disciplinas);

        dbContext.SaveChanges();
    }
}
