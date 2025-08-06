using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Testes.Integracao.Compartilhado;

namespace GeradorDeTestes.Testes.Integracao.ModuloDisciplina;

[TestClass]
[TestCategory("Testes de Integração de Disciplina")]
public sealed class RepositorioDisciplinaEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Disciplina_Corretamente()
    {
        var disciplina = new Disciplina("Matemática");
        repositorioDisciplina?.Cadastrar(disciplina);
        dbContext?.SaveChanges();

        var registroSelecionado = repositorioDisciplina?.SelecionarRegistroPorId(disciplina.Id);

        Assert.AreEqual(disciplina, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Disciplina_Corretamente()
    {
        var disciplina = new Disciplina("Matemática");
        repositorioDisciplina?.Cadastrar(disciplina);
        dbContext?.SaveChanges();

        var disciplinaEditada = new Disciplina("Física");

        var conseguiuEditar = repositorioDisciplina?.Editar(disciplina.Id, disciplinaEditada);
        dbContext?.SaveChanges();

        var registroSelecionado = repositorioDisciplina?.SelecionarRegistroPorId(disciplina.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(disciplina, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Disciplina_Corretamente()
    {
        var disciplina = new Disciplina("Matemática");
        repositorioDisciplina?.Cadastrar(disciplina);
        dbContext?.SaveChanges();

        var conseguiuExcluir = repositorioDisciplina?.Excluir(disciplina.Id);
        dbContext?.SaveChanges();

        var registroSelecionado = repositorioDisciplina?.SelecionarRegistroPorId(disciplina.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Disciplinas_Corretamente()
    {
        var disciplina1 = new Disciplina("Matemática");
        var disciplina2 = new Disciplina("Português");
        var disciplina3 = new Disciplina("Ciências");

        repositorioDisciplina?.Cadastrar(disciplina1);
        repositorioDisciplina?.Cadastrar(disciplina2);
        repositorioDisciplina?.Cadastrar(disciplina3);
        
        dbContext?.SaveChanges();

        List<Disciplina> disciplinasEsperadas = [disciplina1, disciplina2, disciplina3];

        var registrosSelecionados = repositorioDisciplina?.SelecionarRegistros();

        CollectionAssert.AreEquivalent(disciplinasEsperadas, registrosSelecionados);
    }
}
