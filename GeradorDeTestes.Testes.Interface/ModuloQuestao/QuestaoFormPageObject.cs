using GeradorDeTestes.Testes.Interface.ModuloMateria;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace GeradorDeTestes.Testes.Interface.ModuloQuestao;

public class QuestaoFormPageObject
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public QuestaoFormPageObject(IWebDriver driver)
    {
        this.driver = driver;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d => d.FindElement(By.CssSelector("form")).Displayed);
    }

    public QuestaoFormPageObject PreencherEnunciado(string enunciado)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Enunciado")).Displayed &&
            d.FindElement(By.Id("Enunciado")).Enabled
        );

        var inputNome = driver.FindElement(By.Id("Enunciado"));
        inputNome.Clear();
        inputNome.SendKeys(enunciado);

        return this;
    }

    public QuestaoFormPageObject PreencherResposta(string resposta, bool correta = false)
    {
        wait.Until(d =>
            d.FindElement(By.Id("Resposta")).Displayed &&
            d.FindElement(By.Id("Resposta")).Enabled
        );

        var inputResposta = driver.FindElement(By.Id("Resposta"));
        var checkboxCorreta = driver.FindElement(By.Id("Correta"));
        
        if (checkboxCorreta.Selected != correta)
            checkboxCorreta.Click();
        
        inputResposta.Clear();
        inputResposta.SendKeys(resposta);

        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnAdicionar']"))).Click();

        wait.Timeout = TimeSpan.FromSeconds(2);

        return this;
    }

    public QuestaoFormPageObject RemoverRespostas(int quantidade)
    {
        for (int i = 0; i < quantidade; i++)
        {
            wait.Until(d =>
            d.FindElement(By.CssSelector($"button[data-se='btnRemover']")).Displayed &&
            d.FindElement(By.CssSelector($"button[data-se='btnRemover']")).Enabled
            );

            wait.Until(d => d.FindElement(By.CssSelector($"button[data-se='btnRemover']"))).Click();
            
            wait.Timeout = TimeSpan.FromSeconds(2);
        }
        
        return this;
    }

    public QuestaoFormPageObject SelecionarMateria(string materia)
    {
        wait.Until(d =>
            d.FindElement(By.Id("MateriaId")).Displayed &&
            d.FindElement(By.Id("MateriaId")).Enabled
        );

        var select = new SelectElement(driver.FindElement(By.Id("MateriaId")));
        select.SelectByText(materia);

        return this;
    }

    public QuestaoIndexPageObject Confirmar(bool deveScrollar = false)
    {
        if (deveScrollar)
            new Actions(driver).ScrollByAmount(0, 300).Perform();

        wait.Timeout = TimeSpan.FromSeconds(2);

        wait.Until(d => d.FindElement(By.CssSelector("button[data-se='btnConfirmar']"))).Click();

        return new QuestaoIndexPageObject(driver);
    }
}
