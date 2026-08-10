using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Domain;
using Microsoft.EntityFrameworkCore;

namespace Huebeiro.BrazilianCup.Infrastructure.Persistence.Repositories;

public class TeamRepository(BrazilianCupDbContext context) : ITeamRepository
{
    public async Task<Team?> GetByIdAsync(short id)
    {
        var entity = await context.Teams
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity is null ? null : ToDomain(entity);
    }

    public async Task<IReadOnlyList<Team>> GetAllAsync()
    {
        var entities = await context.Teams
            .AsNoTracking() // A lista de Standings não será mutável
            .ToListAsync();

        return entities
            .Select(ToDomain)
            .ToList();
    }

    public async Task AddRangeAsync(IEnumerable<Team> teams)
    {
        var entities = teams.Select(t =>
            new Entities.Team()
            {
                Name = t.Name,
                Wins = t.Wins,
                Draws = t.Draws,
                Losses = t.Losses,
                GoalsFor = t.GoalsFor,
                GoalsAgainst = t.GoalsAgainst
            });
        await context.Teams.AddRangeAsync(entities);
    }

    public void Update(Team team)
    {
        var entity = context.Teams
            .First(x => x.Id == team.Id);

        entity.Wins = team.Wins;
        entity.Draws = team.Draws;
        entity.Losses = team.Losses;
        entity.GoalsFor = team.GoalsFor;
        entity.GoalsAgainst = team.GoalsAgainst;
    }

    private static Team ToDomain(Entities.Team entity)
    {
        return new Team(
            entity.Id,
            entity.Name,
            entity.Wins,
            entity.Draws,
            entity.Losses,
            entity.GoalsFor,
            entity.GoalsAgainst);
    }
}