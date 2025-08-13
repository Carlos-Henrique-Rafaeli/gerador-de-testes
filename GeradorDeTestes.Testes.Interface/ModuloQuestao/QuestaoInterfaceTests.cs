using GeradorDeTestes.Testes.Interface.Compartilhado;
using GeradorDeTestes.Testes.Interface.ModuloDisciplina;
using GeradorDeTestes.Testes.Interface.ModuloMateria;

namespace GeradorDeTestes.Testes.Interface.ModuloQuestao;

[TestClass]
[TestCategory("Testes de interface de Questão")]
public sealed class QuestaoInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Questao_Corretamente()
    {
        //Arrange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!).IrPara(enderecoBase);

        disciplinaIndex
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        var materiaIndex = new MateriaIndexPageObject(driver!)
            .IrPara(enderecoBase);

        materiaIndex
            .ClickCadastrar()
            .PreencherNome("Quatro Operações")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarDisciplina("Matemática")
            .Confirmar();

        //Act
        var questaoIndex = new QuestaoIndexPageObject(driver!)
            .IrPara(enderecoBase);

        questaoIndex.ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);

        //Assert
        Assert.IsTrue(questaoIndex.ContemQuestao("Qual é a soma de 2 + 2?"));
    }

    [TestMethod]
    public void Deve_Editar_Questao_Corretamente()
    {
        //Arrange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        var materiaIndex = new MateriaIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherNome("Quatro Operações")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarDisciplina("Matemática")
            .Confirmar();

        var questaoIndex = new QuestaoIndexPageObject(driver!)
            .IrPara(enderecoBase);

        questaoIndex.ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);

        //Act
        questaoIndex
           .ClickEditar()
           .PreencherEnunciado("Qual é a soma de 3 + 3?")
           .SelecionarMateria("Quatro Operações")
           .RemoverRespostas(4)
           .PreencherResposta("3")
           .PreencherResposta("4")
           .PreencherResposta("5")
           .PreencherResposta("6", true)
           .Confirmar(true);

        //Assert
        Assert.IsTrue(questaoIndex.ContemQuestao("Qual é a soma de 3 + 3?"));
    }

    [TestMethod]
    public void Deve_Excluir_Questao_Corretamente()
    {
        //Arrange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();
        
        var materiaIndex = new MateriaIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherNome("Quatro Operações")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarDisciplina("Matemática")
            .Confirmar();
        
        var questaoIndex = new QuestaoIndexPageObject(driver!)
            .IrPara(enderecoBase);
        
        questaoIndex.ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);
        
        //Act
        questaoIndex
            .ClickExcluir()
            .Confirmar();
        
        //Assert
        Assert.IsFalse(questaoIndex.ContemQuestao("Qual é a soma de 2 + 2?"));
    }
}
