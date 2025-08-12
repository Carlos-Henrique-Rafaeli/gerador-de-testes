using GeradorDeTestes.Testes.Interface.Compartilhado;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloDisciplina;

[TestClass]
[TestCategory("Testes de interface de Disciplina")]
public sealed class DisciplinaInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Disciplina_Corretamente()
    {
        //Arange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase);

        //Act
        disciplinaIndex
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        //Assert
        Assert.IsTrue(disciplinaIndex.ContemDisciplina("Matemática"));
    }

    [TestMethod]
    public void Deve_Editar_Disciplina_Corretamente()
    {
        //Arrange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase);

        disciplinaIndex
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        //Act
        disciplinaIndex
            .ClickEditar()
            .PreencherNome("Matemática Editada")
            .Confirmar();

        //Assert
        Assert.IsTrue(disciplinaIndex.ContemDisciplina("Matemática Editada"));
    }

    [TestMethod]
    public void Deve_Excluir_Disciplina_Corretamente()
    {
        //Arrange
        var disciplinaIndex = new DisciplinaIndexPageObject(driver!)
            .IrPara(enderecoBase);

        disciplinaIndex
            .ClickCadastrar()
            .PreencherNome("Matemática")
            .Confirmar();

        //Act
        disciplinaIndex
            .ClickExcluir()
            .Confirmar();

        //Assert
        Assert.IsFalse(disciplinaIndex.ContemDisciplina("Matemática"));
    }
}
