using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using TestScraper;

var stopWatch = Stopwatch.StartNew();

var url = "https://www.futebolinterior.com.br/campeonato/brasileirao-serie-a-2026/";
var tableClass = "table-classification--expansive";

Console.WriteLine($"Navigating to page: '{url}'...\n");

var options = new ChromeOptions()
{
    PageLoadStrategy = PageLoadStrategy.Eager,
};

options.AddArgument("--headless");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");

using var driver = new ChromeDriver(options);

driver.Navigate().GoToUrl(url);

var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
{
    PollingInterval = TimeSpan.FromMilliseconds(250),
};

var table = wait.Until(d => 
    d.FindElement(
        By.CssSelector($"table.{tableClass}")));

Console.WriteLine($"Page title: '{driver.Title}'");
Console.WriteLine($"Table found with classname: '{tableClass}'");

var rows = table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

Console.WriteLine($"Total teams found: {rows.Count}");
if (rows.Count != 20)
    throw new Exception("Wrong format.");

var standings = new List<Standing>();

foreach (var row in rows)
{
    var cells = row.FindElements(By.TagName("td"));

    var standing = new Standing(cells.Select(c => c.Text).ToArray());
    Console.WriteLine($"Standing found: {standing}");
    standings.Add(standing);
}

stopWatch.Stop();
Console.WriteLine($"\n\nExecution finished. Elapsed time: {stopWatch.Elapsed}");