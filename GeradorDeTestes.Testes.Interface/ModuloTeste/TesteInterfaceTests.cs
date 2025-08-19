using GeradorDeTestes.Testes.Interface.Compartilhado;
using GeradorDeTestes.Testes.Interface.ModuloDisciplina;
using GeradorDeTestes.Testes.Interface.ModuloMateria;
using GeradorDeTestes.Testes.Interface.ModuloQuestao;

namespace GeradorDeTestes.Testes.Interface.ModuloTeste;


[TestClass]
[TestCategory("Testes de interface de Teste")]
public class TesteInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Criar_Teste_Normal_Corretamente()
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
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);

        questaoIndex
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 4 + 4?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("8", true)
            .PreencherResposta("12")
            .PreencherResposta("16")
            .PreencherResposta("32")
            .Confirmar(true);

        //Act
        var testeIndex = new TesteIndexPageObject(driver!)
            .IrPara(enderecoBase);
        
        testeIndex
            .ClickCadastrar()
            .PreencherTitulo("Prova de Matemática")
            .SelecionarDisciplina("Matemática")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarQuantidadeQuestoes(2)
            .ConfirmarPrimeiraEtapa()
            .SortearQuestoes()
            .ConfirmarSegundaEtapa();

        //Assert
        Assert.IsTrue(testeIndex.ContemTeste("Prova de Matemática"));
    }

    [TestMethod]
    public void Deve_Criar_Teste_Recuperação_Corretamente()
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

        materiaIndex
            .ClickCadastrar()
            .PreencherNome("Função Quadrática")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarDisciplina("Matemática")
            .Confirmar();

        var questaoIndex = new QuestaoIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);

        questaoIndex
            .ClickCadastrar()
            .PreencherEnunciado("Qual das seguintes funções representa uma função quadrática?")
            .SelecionarMateria("Função Quadrática")
            .PreencherResposta("f(x) = x² - 5x + 6", true)
            .PreencherResposta("f(x) = 2x + 3")
            .PreencherResposta("f(x) = 3x - 1")
            .PreencherResposta("f(x) = 2/x")
            .Confirmar(true);

        //Act
        var testeIndex = new TesteIndexPageObject(driver!)
            .IrPara(enderecoBase);

        testeIndex
            .ClickCadastrar()
            .PreencherTitulo("Prova de Matemática")
            .SelecionarDisciplina("Matemática")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarRecuperação()
            .SelecionarQuantidadeQuestoes(2)
            .ConfirmarPrimeiraEtapa()
            .SortearQuestoes()
            .ConfirmarSegundaEtapa();

        //Assert
        Assert.IsTrue(testeIndex.ContemTeste("Prova de Matemática"));
    }

    [TestMethod]
    public void Deve_Duplicar_Teste_Corretamente()
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
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);

        questaoIndex
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 4 + 4?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("8", true)
            .PreencherResposta("12")
            .PreencherResposta("16")
            .PreencherResposta("32")
            .Confirmar(true);

        var testeIndex = new TesteIndexPageObject(driver!)
            .IrPara(enderecoBase);

        testeIndex
            .ClickCadastrar()
            .PreencherTitulo("Prova de Matemática")
            .SelecionarDisciplina("Matemática")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarQuantidadeQuestoes(2)
            .ConfirmarPrimeiraEtapa()
            .SortearQuestoes()
            .ConfirmarSegundaEtapa();

        //Act
        testeIndex
            .ClickDuplicar()
            .PreencherTitulo("Prova de Matemática Duplicada")
            .SortearQuestoes()
            .ConfirmarSegundaEtapa();

        //Assert
        Assert.IsTrue(testeIndex.ContemTeste("Prova de Matemática"));
        Assert.IsTrue(testeIndex.ContemTeste("Prova de Matemática Duplicada"));
    }

    [TestMethod]
    public void Deve_Excluir_Teste_Corretamente()
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
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 2 + 2?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("1")
            .PreencherResposta("2")
            .PreencherResposta("3")
            .PreencherResposta("4", true)
            .Confirmar(true);

        questaoIndex
            .ClickCadastrar()
            .PreencherEnunciado("Qual é a soma de 4 + 4?")
            .SelecionarMateria("Quatro Operações")
            .PreencherResposta("8", true)
            .PreencherResposta("12")
            .PreencherResposta("16")
            .PreencherResposta("32")
            .Confirmar(true);

        var testeIndex = new TesteIndexPageObject(driver!)
            .IrPara(enderecoBase);

        testeIndex
            .ClickCadastrar()
            .PreencherTitulo("Prova de Matemática")
            .SelecionarDisciplina("Matemática")
            .SelecionarSerie("2º Ano - EF")
            .SelecionarQuantidadeQuestoes(2)
            .ConfirmarPrimeiraEtapa()
            .SortearQuestoes()
            .ConfirmarSegundaEtapa();

        //Act
        testeIndex
            .ClickExcluir()
            .ConfirmarSegundaEtapa();

        //Assert
        Assert.IsFalse(testeIndex.ContemTeste("Prova de Matemática"));
    }
}
