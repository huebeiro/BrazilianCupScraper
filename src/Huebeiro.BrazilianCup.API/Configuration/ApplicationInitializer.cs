using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Application.Services;

namespace Huebeiro.BrazilianCup.API.Configuration;

public class ApplicationInitializer(
    ITeamRepository teamRepository,
    InitializeTeamsService initializeTeamsService)
{
    /// <summary>
    /// Método para a verificação de dados de Time e inicialização do Scraper
    /// </summary>
    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        var teams = await teamRepository.GetAllAsync();

        if (teams.Count > 0)
            return;

        await initializeTeamsService.ExecuteAsync(cancellationToken);
    }
}