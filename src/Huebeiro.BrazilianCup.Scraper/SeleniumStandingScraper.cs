using Huebeiro.BrazilianCup.Domain;
using Huebeiro.BrazilianCup.Scraper.Exceptions;
using Huebeiro.BrazilianCup.Scraper.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Huebeiro.BrazilianCup.Scraper;

public class SeleniumStandingScraper : IStandingScraper
{

    // TODO mover para appsettings
    /// <summary>
    /// Url do site a ser raspado
    /// </summary>
    private const string Url =
        "https://www.futebolinterior.com.br/campeonato/brasileirao-serie-a-2026/";

    /// <summary>
    /// Classe CSS da tabela a ser lida
    /// </summary>
    private const string TableClass =
        "table-classification--expansive";

    /// <summary>
    /// Quantidade total de times
    /// </summary>
    private const int TotalTeams = 20;

    // Quantidade de colunas na linha de um time para validação
    private const int TotalCells = 12;

    // Indexes das colunas da tabela a ser lida
    private const int NameIndex = 1;
    private const int WinsIndex = 4;
    private const int DrawsIndex = 5;
    private const int LossesIndex = 6;
    private const int GoalsForIndex = 7;
    private const int GoalsAgainstIndex = 8;

    public async Task<List<Team>> ScrapeAsync(
        CancellationToken cancellationToken = default)
    {
        var options = new ChromeOptions
        {
            PageLoadStrategy = PageLoadStrategy.Eager // Apenas esperar carregamento inicial do DOM
        };

        options.AddArgument("--headless"); // Desabilitando interface gráfica do navegador
        options.AddArgument("--no-sandbox"); // Desabilitando mecanismo de sandbox do Chrome
        options.AddArgument("--disable-dev-shm-usage"); // Desabilitando memória /dev/shm do container

        using var driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl(Url);

        var wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(3))
        {
            PollingInterval = TimeSpan.FromMilliseconds(250)
        };

        var table = wait.Until(driver =>
            driver.FindElement(
                By.CssSelector($"table.{TableClass}")));

        var rows = table
            .FindElement(By.TagName("tbody"))
            .FindElements(By.TagName("tr"));

        if (rows.Count != TotalTeams)
            throw new WrongStandingFormatException(
                $"Expected {TotalTeams} teams, but found {rows.Count}.");

        var teams = new List<Team>();

        foreach (var row in rows)
        {
            cancellationToken.ThrowIfCancellationRequested(); // Estoura exceção caso cancelamento for requisitado

            var cells = row.FindElements(By.TagName("td"));

            var team = CreateTeam(cells);

            teams.Add(team);
        }

        // Ordenando a lista pelo nome de time para inserção na base
        return teams.OrderBy(t => t.Name).ToList();
    }

    private static Team CreateTeam(
        IReadOnlyCollection<IWebElement> cells)
    {
        var cellsText = cells
            .Select(cell => cell.Text)
            .ToArray();

        if (cellsText.Length != TotalCells)
            throw new WrongStandingFormatException($"Wrong table row format. Found {cellsText.Length} cells.");

        return new Team(
            cellsText[NameIndex],
            ParseCell(cellsText[WinsIndex]),
            ParseCell(cellsText[DrawsIndex]),
            ParseCell(cellsText[LossesIndex]),
            ParseCell(cellsText[GoalsForIndex]),
            ParseCell(cellsText[GoalsAgainstIndex]));
    }
    private static short ParseCell(string text) =>
        short.TryParse(text, out short parsed)
            ? parsed
            : throw new WrongStandingFormatException($"Cell '{text}' is not a valid short.");
}