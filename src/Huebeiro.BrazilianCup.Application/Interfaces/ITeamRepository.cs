using Huebeiro.BrazilianCup.Domain;

namespace Huebeiro.BrazilianCup.Application.Interfaces;

public interface ITeamRepository
{
    Task AddRangeAsync(IEnumerable<Team> teams);
    Task<Team?> GetByIdAsync(short id);
    Task<IReadOnlyList<Team>> GetAllAsync(); // Utilizando ReadOnlyList para indexação de posições
    void Update(Team team);
}