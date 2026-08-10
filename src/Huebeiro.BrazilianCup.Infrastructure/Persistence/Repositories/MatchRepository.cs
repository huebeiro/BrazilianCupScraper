using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Domain;

namespace Huebeiro.BrazilianCup.Infrastructure.Persistence.Repositories;

public class MatchRepository(BrazilianCupDbContext context) : IMatchRepository
{
    public void Add(Match match)
    {
        var entity = new Entities.Match
        {
            HomeTeamId = match.HomeTeam.Id,
            AwayTeamId = match.AwayTeam.Id,
            HomeGoals = match.HomeGoals,
            AwayGoals = match.AwayGoals
        };

        context.Matches.Add(entity);
    }
}