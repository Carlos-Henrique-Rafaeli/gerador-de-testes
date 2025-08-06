using DotNet.Testcontainers.Containers;
using FizzWare.NBuilder;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;
using Testcontainers.PostgreSql;

namespace GeradorDeTestes.Testes.Integracao.Compartilhado;

[TestClass]
public abstract class TestFixture
{
    protected GeradorDeTestesDbContext? dbContext;

    protected RepositorioTesteEmOrm? repositorioTeste;
    protected RepositorioQuestaoEmOrm? repositorioQuestao;
    protected RepositorioMateriaEmOrm? repositorioMateria;
    protected RepositorioDisciplinaEmOrm? repositorioDisciplina;

    private static IDatabaseContainer? container;

    [AssemblyInitialize]
    public static async Task Setup(TestContext _)
    {
        container = new PostgreSqlBuilder()
              .WithImage("postgres:16")
              .WithName("gerador-de-testes-testdb")
              .WithDatabase("GeradorDeTestesDb")
              .WithUsername("postgres")
              .WithPassword("YourStrongPassword")
              .WithCleanUp(true)
              .Build();

        await InicializarBancoDadosAsync(container);
    }

    [AssemblyCleanup]
    public static async Task Teardown()
    {
        await EncerrarBancoDadosAsync();
    }

    [TestInitialize]
    public void ConfigurarTestes()
    {
        if (container is null)
            throw new ArgumentNullException("O banco de dados não foi inicializado.");

        dbContext = GeradorDeTestesDbContextFactory.CriarDbContext(container.GetConnectionString());

        ConfigurarTabelas(dbContext);

        repositorioTeste = new RepositorioTesteEmOrm(dbContext);
        repositorioQuestao = new RepositorioQuestaoEmOrm(dbContext);
        repositorioDisciplina = new RepositorioDisciplinaEmOrm(dbContext);
        repositorioMateria = new RepositorioMateriaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Disciplina>(repositorioDisciplina.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Disciplina>>(repositorioDisciplina.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Materia>(repositorioMateria.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Materia>>(repositorioMateria.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Questao>(repositorioQuestao.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Questao>>(repositorioQuestao.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Teste>(repositorioTeste.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Teste>>(repositorioTeste.CadastrarEntidades);
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

    private static async Task InicializarBancoDadosAsync(IDatabaseContainer container)
    {
        await container.StartAsync();
    }

    private static async Task EncerrarBancoDadosAsync()
    {
        if (container is null)
            throw new ArgumentNullException("O Banco de dados não foi inicializado.");

        await container.StopAsync();
        await container.DisposeAsync();
    }
}
