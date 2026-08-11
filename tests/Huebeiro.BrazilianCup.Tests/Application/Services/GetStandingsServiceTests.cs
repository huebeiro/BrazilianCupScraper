using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Application.Services;
using Huebeiro.BrazilianCup.Domain;
using Moq;

namespace Huebeiro.BrazilianCup.Tests.Application.Services;

public class GetStandingsServiceTests
{
    private static GetStandingsService StartServiceFromList(List<Team> teams)
    {
        var repository = new Mock<ITeamRepository>();

        repository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(teams);

        return new GetStandingsService(repository.Object);
    }
    /*1. Pontos; ok
        2. Vitórias;
        3. Saldo de gols;
        4. Gols marcados;
        5. Nome do time.*/

    /// <summary>
    /// Testando a classificação por pontos
    /// </summary>
    [Fact]
    public async Task Standings_OrderPoints()
    {
        // Lista para mock de TeamRepository
        var teams = new List<Team>
        {
            // Id, Name,     W, D, L, GF, GA    
            new(1, "Team A", 4, 1, 0, 12, 7),
            new(2, "Team B", 3, 1, 0, 10, 5),
            new(3, "Team C", 2, 1, 0, 10, 5),
            new(4, "Team D", 1, 1, 0, 12, 9)
        };

        var service = StartServiceFromList(teams);

        var result = await service.ExecuteAsync();
        var teamList = result.Select(x => x.TeamName);

        Assert.Equal(
            ["Team A", "Team B", "Team C", "Team D"],
            teamList);
    }

    [Fact]
    public async Task Standings_OrderWins()
    {
        // Lista para mock de TeamRepository
        var teams = new List<Team>
        {
            // Id, Name,     W, D, L, GF, GA    
            new(1, "Team A", 4, 0, 0, 12, 7),
            new(2, "Team B", 3, 3, 0, 10, 5),
            new(3, "Team C", 2, 6, 0, 10, 5),
            new(4, "Team D", 1, 9, 0, 12, 9)
        };

        var service = StartServiceFromList(teams);

        var result = await service.ExecuteAsync();
        var teamList = result.Select(x => x.TeamName);

        Assert.Equal(
            ["Team A", "Team B", "Team C", "Team D"],
            teamList);
    }

    [Fact]
    public async Task Standings_GoalsDifference()
    {
        // Lista para mock de TeamRepository
        var teams = new List<Team>
        {
            // Id, Name,     W, D, L, GF, GA    
            new(1, "Team A", 4, 0, 0, 10, 0),
            new(2, "Team B", 4, 0, 0, 10, 1),
            new(3, "Team C", 4, 0, 0, 10, 2),
            new(4, "Team D", 4, 0, 0, 10, 3)
        };

        var service = StartServiceFromList(teams);

        var result = await service.ExecuteAsync();
        var teamList = result.Select(x => x.TeamName);

        Assert.Equal(
            ["Team A", "Team B", "Team C", "Team D"],
            teamList);
    }

    [Fact]
    public async Task Standings_GoalsFor()
    {
        // Lista para mock de TeamRepository
        var teams = new List<Team>
        {
            // Id, Name,     W, D, L, GF, GA    
            new(1, "Team A", 4, 0, 0, 13, 3),
            new(2, "Team B", 4, 0, 0, 12, 2),
            new(3, "Team C", 4, 0, 0, 11, 1),
            new(4, "Team D", 4, 0, 0, 10, 0)
        };

        var service = StartServiceFromList(teams);

        var result = await service.ExecuteAsync();
        var teamList = result.Select(x => x.TeamName);

        Assert.Equal(
            ["Team A", "Team B", "Team C", "Team D"],
            teamList);
    }

    [Fact]
    public async Task Standings_TeamName()
    {
        // Lista para mock de TeamRepository
        var teams = new List<Team>
        {
            // Id, Name,     W, D, L, GF, GA    
            new(1, "Team A", 4, 0, 0, 10, 0),
            new(2, "Team B", 4, 0, 0, 10, 0),
            new(3, "Team C", 4, 0, 0, 10, 0),
            new(4, "Team D", 4, 0, 0, 10, 0)
        };

        var service = StartServiceFromList(teams);

        var result = await service.ExecuteAsync();
        var teamList = result.Select(x => x.TeamName);

        Assert.Equal(
            ["Team A", "Team B", "Team C", "Team D"],
            teamList);
    }
}