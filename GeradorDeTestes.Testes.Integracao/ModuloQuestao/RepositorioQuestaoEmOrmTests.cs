using FizzWare.NBuilder;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Testes.Integracao.Compartilhado;

namespace GeradorDeTestes.Testes.Integracao.ModuloQuestao;

[TestClass]
[TestCategory("Testes de Integração de Questão")]
public sealed class RepositorioQuestaoEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Questao_Corretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Química")
            .Persist();

        var materia = Builder<Materia>.CreateNew()
            .With(m => m.Nome = "Química orgânica")
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questao = new Questao("Quantos carbonos tem em um benzeno?", materia);

        //Act
        repositorioQuestao?.Cadastrar(questao);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioQuestao?.SelecionarRegistroPorId(questao.Id);

        Assert.AreEqual(questao, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Questao_Corretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Química")
            .Persist();

        var materia = Builder<Materia>.CreateNew()
            .With(m => m.Nome = "Química orgânica")
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questao = new Questao("Quantos carbonos tem em um benzeno?", materia);

        repositorioQuestao?.Cadastrar(questao);
        dbContext?.SaveChanges();

        var questaoEditada = new Questao("O que é um anel aromático?", materia);

        //Act
        var conseguiuEditar = repositorioQuestao?.Editar(questao.Id, questaoEditada);
        dbContext?.SaveChanges();

        //Assert
        var registroSelecionado = repositorioQuestao?.SelecionarRegistroPorId(questao.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(questao, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Questao_Corretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Química")
            .Persist();

        var materia = Builder<Materia>.CreateNew()
            .With(m => m.Nome = "Química orgânica")
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questao = new Questao("Quantos carbonos tem em um benzeno?", materia);

        repositorioQuestao?.Cadastrar(questao);
        dbContext?.SaveChanges();

        //Act
        var conseguiuExcluir = repositorioQuestao?.Excluir(questao.Id);
        dbContext?.SaveChanges();

        //Assert
        var registroSelecionado = repositorioQuestao?.SelecionarRegistroPorId(questao.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Questoes_Corretamente()
    {
        //Arrange
        var disciplina = Builder<Disciplina>.CreateNew()
            .With(d => d.Nome = "Química")
            .Persist();

        var materias = Builder<Materia>.CreateListOfSize(3)
            .All()
            .With(m => m.Disciplina = disciplina)
            .Persist();

        var questao = new Questao("Quantos carbonos tem em um benzeno?", materias[0]);
        var questao2 = new Questao("O que é um anel aromático?", materias[1]);
        var questao3 = new Questao("Um pentano tem quantos carbonos?", materias[2]);

        List<Questao> registrosEsperados = [questao, questao2, questao3];

        repositorioQuestao?.CadastrarEntidades(registrosEsperados);
        dbContext?.SaveChanges();

        //Act
        var registrosRecebidos = repositorioQuestao?.SelecionarRegistros();

        //Assert
        CollectionAssert.AreEquivalent(registrosEsperados, registrosRecebidos);
    }
}
