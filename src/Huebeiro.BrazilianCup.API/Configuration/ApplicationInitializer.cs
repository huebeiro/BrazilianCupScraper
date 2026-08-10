using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Application.Services;

namespace Huebeiro.BrazilianCup.API.Configuration;

public class ApplicationInitializer(
    ITeamRepository teamRepository,
    InitializeTeamsService initializeTeamsService)
{
    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        var teams = await teamRepository.GetAllAsync();

        if (teams.Count > 0)
            return;

        await initializeTeamsService.ExecuteAsync(cancellationToken);
    }
}