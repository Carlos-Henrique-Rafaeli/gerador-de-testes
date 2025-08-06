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
        //Arrange
        var disciplina = new Disciplina("Matemática");

        //Act
        repositorioDisciplina?.Cadastrar(disciplina);
        dbContext?.SaveChanges();

        //Assert
        var registroSelecionado = repositorioDisciplina?.SelecionarRegistroPorId(disciplina.Id);

        Assert.AreEqual(disciplina, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Disciplina_Corretamente()
    {
        //Arrange
        var disciplina = new Disciplina("Matemática");
        repositorioDisciplina?.Cadastrar(disciplina);
        dbContext?.SaveChanges();

        var disciplinaEditada = new Disciplina("Física");

        //Act
        var conseguiuEditar = repositorioDisciplina?.Editar(disciplina.Id, disciplinaEditada);
        dbContext?.SaveChanges();

        //Assert
        var registroSelecionado = repositorioDisciplina?.SelecionarRegistroPorId(disciplina.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(disciplina, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Disciplina_Corretamente()
    {
        //Arrange
        var disciplina = new Disciplina("Matemática");
        repositorioDisciplina?.Cadastrar(disciplina);
        dbContext?.SaveChanges();

        //Act
        var conseguiuExcluir = repositorioDisciplina?.Excluir(disciplina.Id);
        dbContext?.SaveChanges();

        //Assert
        var registroSelecionado = repositorioDisciplina?.SelecionarRegistroPorId(disciplina.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Disciplinas_Corretamente()
    {
        //Arrange
        var disciplina1 = new Disciplina("Matemática");
        var disciplina2 = new Disciplina("Português");
        var disciplina3 = new Disciplina("Ciências");

        List<Disciplina> disciplinasEsperadas = [disciplina1, disciplina2, disciplina3];

        repositorioDisciplina?.CadastrarEntidades(disciplinasEsperadas);
        dbContext?.SaveChanges();

        //Act
        var registrosSelecionados = repositorioDisciplina?.SelecionarRegistros();

        //Assert
        CollectionAssert.AreEquivalent(disciplinasEsperadas, registrosSelecionados);
    }
}
