using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Scraper.Interfaces;

namespace Huebeiro.BrazilianCup.Application.Services;

/// <summary>
/// Serviço para inicialização do Scraper implementado
/// </summary>
public class InitializeTeamsService(
    IStandingScraper standingScraper,
    ITeamRepository teamRepository,
    IUnitOfWork unitOfWork)
{
    public async Task ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var teams = await standingScraper.ScrapeAsync(cancellationToken);

        await teamRepository.AddRangeAsync(teams);

        await unitOfWork.SaveChangesAsync();
    }
}