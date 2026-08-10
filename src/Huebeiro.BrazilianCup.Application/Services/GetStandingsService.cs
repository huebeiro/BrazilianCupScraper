using Huebeiro.BrazilianCup.Application.DTO;
using Huebeiro.BrazilianCup.Application.Interfaces;

namespace Huebeiro.BrazilianCup.Application.Services;

public class GetStandingsService(ITeamRepository teamRepository)
{
    public async Task<IReadOnlyList<StandingResponse>> ExecuteAsync()
    {
        var teams = await teamRepository.GetAllAsync();

        /* Searching for standings then orderning by criteria:
         * 1. Points;
         * 2. Wins;
         * 3. Goals Difference;
         * 4. Goals For;
         * 5. Team Name.
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