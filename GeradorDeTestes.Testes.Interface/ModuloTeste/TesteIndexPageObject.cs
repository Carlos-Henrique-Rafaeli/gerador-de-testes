using GeradorDeTestes.Testes.Interface.ModuloQuestao;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloTeste;

public class TesteIndexPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public TesteIndexPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public TesteIndexPageObject IrPara(string enderecoBase)
    {
        driver?.Navigate().GoToUrl(Path.Combine(enderecoBase, "testes"));

        return this;
    }

    public TesteFormPageObject ClickCadastrar()
    {
        wait.Until(d => d?.FindElement(By.CssSelector("a[data-se='btnCadastrar']"))).Click();

        return new TesteFormPageObject(driver!);
    }

    public TesteFormPageObject ClickDuplicar()
    {
        wait.Until(d => d?.FindElement(By.CssSelector(".card a[title='Duplicar']"))).Click();

        return new TesteFormPageObject(driver!);
    }

    public TesteFormPageObject ClickExcluir()
    {
        wait.Until(d => d?.FindElement(By.CssSelector(".card a[title='Exclusão']"))).Click();

        return new TesteFormPageObject(driver!);
    }

    public bool ContemTeste(string nome)
    {
        wait.Until(d => d.FindElement(By.CssSelector("a[data-se='btnCadastrar']")).Displayed);

        return driver.PageSource.Contains(nome);
    }
}
