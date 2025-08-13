using GeradorDeTestes.Testes.Interface.ModuloMateria;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloQuestao;

public class QuestaoIndexPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public QuestaoIndexPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public QuestaoIndexPageObject IrPara(string enderecoBase)
    {
        driver?.Navigate().GoToUrl(Path.Combine(enderecoBase, "questoes"));

        return this;
    }

    public QuestaoFormPageObject ClickCadastrar()
    {
        driver?.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Click();

        return new QuestaoFormPageObject(driver!);
    }

    public QuestaoFormPageObject ClickEditar()
    {
        driver?.FindElement(By.CssSelector(".card a[title='Edição']")).Click();

        return new QuestaoFormPageObject(driver!);
    }

    public QuestaoFormPageObject ClickExcluir()
    {
        driver?.FindElement(By.CssSelector(".card a[title='Exclusão']")).Click();

        return new QuestaoFormPageObject(driver!);
    }

    public bool ContemQuestao(string nome)
    {
        return driver.PageSource.Contains(nome);
    }
}
