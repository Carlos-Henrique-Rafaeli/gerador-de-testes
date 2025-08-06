using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.Testes.Integracao.Compartilhado;
using FizzWare.NBuilder;

namespace GeradorDeTestes.Testes.Integracao.ModuloMateria;

[TestClass]
[TestCategory("Testes de Integração de Matéria")]
public sealed class RepositorioMateriaEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Materia_Corretamente()
    {
        var disciplina = Builder<Disciplina>.CreateNew().Persist();

        var materia = new Materia("Leis de newton", Serie.PrimeiroAnoEM, disciplina);

        repositorioMateria?.Cadastrar(materia);
        dbContext?.SaveChanges();

        var materiaSelecionada = repositorioMateria?.SelecionarRegistroPorId(materia.Id);

        Assert.AreEqual(materia, materiaSelecionada);
    }

    [TestMethod]
    public void Deve_Editar_Materia_Corretamente()
    {
        var disciplina = Builder<Disciplina>.CreateNew().Persist();
        
        var materia = new Materia("Leis de newton", Serie.PrimeiroAnoEM, disciplina);
        repositorioMateria?.Cadastrar(materia);
        dbContext?.SaveChanges();

        var materiaEditada = new Materia("Leis de newton editada", Serie.PrimeiroAnoEM, disciplina);

        var conseguiuEditar = repositorioMateria?.Editar(materia.Id, materiaEditada);
        dbContext?.SaveChanges();

        var registroSelecionado = repositorioMateria?.SelecionarRegistroPorId(materia.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(materia, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Materia_Corretamente()
    {
        var disciplina = Builder<Disciplina>.CreateNew().Persist();

        var materia = new Materia("Leis de newton", Serie.PrimeiroAnoEM, disciplina);
        repositorioMateria?.Cadastrar(materia);
        dbContext?.SaveChanges();

        var conseguiuExcluir = repositorioMateria?.Excluir(materia.Id);
        dbContext?.SaveChanges();

        var registroSelecionado = repositorioMateria?.SelecionarRegistroPorId(materia.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Materia_Corretamente()
    {
        var disciplina = Builder<Disciplina>.CreateNew().Persist();
        
        var materia1 = new Materia("Leis de newton", Serie.PrimeiroAnoEM, disciplina);
        var materia2 = new Materia("Leis de mendel", Serie.PrimeiroAnoEM, disciplina);
        var materia3 = new Materia("Leis de oppenheimer", Serie.PrimeiroAnoEM, disciplina);

        repositorioMateria?.Cadastrar(materia1);
        repositorioMateria?.Cadastrar(materia2);
        repositorioMateria?.Cadastrar(materia3);
        
        dbContext?.SaveChanges();

        List<Materia> materias = [materia1, materia2, materia3];

        dbContext?.SaveChanges();
        
        var materiasSelecionadas =  repositorioMateria?.SelecionarRegistros();

        CollectionAssert.AreEquivalent(materias, materiasSelecionadas);
    }
}
