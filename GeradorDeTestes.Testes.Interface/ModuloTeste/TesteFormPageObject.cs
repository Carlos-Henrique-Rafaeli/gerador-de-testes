using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Testes.Interface.ModuloQuestao;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloTeste;

public class TesteFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public TesteFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form")).Displayed);
    }

    public TesteFormPageObject PreencherTitulo(string titulo)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Titulo")).Displayed &&
            d.FindElement(By.Id("Titulo")).Enabled
        );

        var inputNome = driver.FindElement(By.Id("Titulo"));
        inputNome.Clear();
        inputNome.SendKeys(titulo);

        return this;
    }

    public TesteFormPageObject SelecionarDisciplina(string disciplina)
    {
        wait.Until(d =>
            d.FindElement(By.Id("DisciplinaId")).Displayed &&
            d.FindElement(By.Id("DisciplinaId")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.Id("DisciplinaId")));
        select.SelectByText(disciplina);

        return this;
    }

    public TesteFormPageObject SelecionarSerie(string serie)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Serie")).Displayed &&
            d.FindElement(By.Id("Serie")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.Id("Serie")));
        select.SelectByText(serie);

        return this;
    }

    public TesteFormPageObject SelecionarQuantidadeQuestoes(int quantidade)
    {
        wait.Until(d =>
            d.FindElement(By.Id("QuantidadeQuestoes")).Displayed &&
            d.FindElement(By.Id("QuantidadeQuestoes")).Enabled
        );

        var select = driver.FindElement(By.Id("QuantidadeQuestoes"));
        select.Clear();
        select.SendKeys(quantidade.ToString());

        return this;
    }

    public TesteFormPageObject SortearQuestoes()
    {
        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnSortear']"))).Click();

        return new TesteFormPageObject(driver);
    }

    public TesteFormPageObject SelecionarRecuperação()
    {
        wait.Until(d =>
            d.FindElement(By.Id("Recuperacao")).Displayed &&
            d.FindElement(By.Id("Recuperacao")).Enabled
        );

        driver.FindElement(By.Id("Recuperacao")).Click();

        return this;
    }

    public TesteFormPageObject ConfirmarPrimeiraEtapa()
    {
        new Actions(driver).ScrollByAmount(0, 300).Perform();

        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        return new TesteFormPageObject(driver);
    }

    public TesteIndexPageObject ConfirmarSegundaEtapa()
    {
        new Actions(driver).ScrollByAmount(0, 300).Perform();

        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        return new TesteIndexPageObject(driver);
    }
}
