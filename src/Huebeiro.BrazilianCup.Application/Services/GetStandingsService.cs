using Huebeiro.BrazilianCup.Application.DTO;
using Huebeiro.BrazilianCup.Application.Interfaces;

namespace Huebeiro.BrazilianCup.Application.Services;

/// <summary>
/// Serviço para a consulta e ordenação da classificação do campeonato
/// </summary>
public class GetStandingsService(ITeamRepository teamRepository)
{
    public async Task<IReadOnlyList<StandingResponse>> ExecuteAsync()
    {
        var teams = await teamRepository.GetAllAsync();

        /* Critérios de classificação:
         * 1. Pontos;
         * 2. Vitórias;
         * 3. Saldo de gols;
         * 4. Gols marcados;
         * 5. Nome do time.
         * */
        var standings = teams
            .OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.Wins)
            .ThenByDescending(x => x.GoalDifference)
            .ThenByDescending(x => x.GoalsFor)
            .ThenBy(x => x.Name)
            .Select((team, index) => new StandingResponse(
                index + 1,
                team.Id,
                team.Name,
                team.Points,
                team.Matches,
                team.Wins,
                team.Draws,
                team.Losses,
                team.GoalsFor,
                team.GoalsAgainst,
                team.GoalDifference))
            .ToList();

        return standings;
    }
}