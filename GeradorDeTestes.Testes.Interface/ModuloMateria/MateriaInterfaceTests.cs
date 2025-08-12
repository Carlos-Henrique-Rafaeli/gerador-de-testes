using GeradorDeTestes.Testes.Interface.Compartilhado;
using GeradorDeTestes.Testes.Interface.ModuloDisciplina;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloMateria;

[TestClass]
[TestCategory("Testes de interface de Materia")]
public sealed class MateriaInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Materia_Corretamente()
    {
        //Arrange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!).IrPara(enderecoBase);

        disciplinaIndex
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        //Act
        var materiaIndex = new MateriaIndexPageObject(driver!)
            .IrPara(enderecoBase);


        materiaIndex
            .ClickCadastrar()
            .PreencherNome("Quatro Operações")
            .SelecionarSerie("2º Ano - EM")
            .SelecionarDisciplina("Matemática")
            .Confirmar();
        //Assert
        Assert.IsTrue(materiaIndex.ContemMateria("Quatro Operações"));
    }

    [TestMethod]
    public void Deve_Editar_Materia_Corretamente()
    {
        //Arrange
        new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        var materiaIndex = new MateriaIndexPageObject(driver!)
            .IrPara(enderecoBase);

        materiaIndex
            .ClickCadastrar()
            .PreencherNome("Quatro Operações")
            .SelecionarSerie("2º Ano - EM")
            .SelecionarDisciplina("Matemática")
            .Confirmar();

        //Act
        materiaIndex
           .ClickEditar()
           .PreencherNome("Quatro Operações Editada")
           .SelecionarSerie("3º Ano - EM")
           .SelecionarDisciplina("Matemática")
           .Confirmar();

        //Assert
        Assert.IsTrue(materiaIndex.ContemMateria("Quatro Operações Editada"));
    }

    [TestMethod]
    public void Deve_Excluir_Materia_Corretamente()
    {
        //Arrange
        new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase)
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        var materiaIndex = new MateriaIndexPageObject(driver!)
            .IrPara(enderecoBase);

        materiaIndex
            .ClickCadastrar()
            .PreencherNome("Quatro Operações")
            .SelecionarSerie("2º Ano - EM")
            .SelecionarDisciplina("Matemática")
            .Confirmar();

        //Act
        materiaIndex
            .ClickExcluir()
            .Confirmar();

        //Assert
        Assert.IsFalse(materiaIndex.ContemMateria("Quatro Operações"));
    }
}
