using Huebeiro.BrazilianCup.Domain;

namespace Huebeiro.BrazilianCup.Scraper.Interfaces;

public interface IStandingScraper
{
    Task<List<Team>> ScrapeAsync(
        CancellationToken cancellationToken = default);
}