using Huebeiro.BrazilianCup.Application.DTO;
using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Application.Services;
using Huebeiro.BrazilianCup.Domain;
using Huebeiro.BrazilianCup.Domain.Exceptions;
using Moq;

namespace Huebeiro.BrazilianCup.Tests.Application.Services;

public class RegisterMatchServiceTests
{
    private static RegisterMatchService StartServiceFromList(List<Team> teams)
    {
        var teamRepository = new Mock<ITeamRepository>();
        var matchRepository = new Mock<IMatchRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        foreach (var team in teams)
        {
            teamRepository
                .Setup(x => x.GetByIdAsync(team.Id))
                .ReturnsAsync(team);
        }

        return new RegisterMatchService(
            teamRepository.Object,
            matchRepository.Object,
            unitOfWork.Object);
    }

    /// <summary>
    /// Teste de registro normal de partida
    /// </summary>
    [Fact]
    public async Task RegisterMatch_LessThan38Matches()
    {
        var teamA = new Team(
            1,
            "Team A",
            10,
            0,
            0,
            10,
            10);

        var teamB = new Team(
            2,
            "Team B",
            10,
            0,
            0,
            10,
            10);

        var service = StartServiceFromList(new List<Team>
        {
            teamA,
            teamB
        });

        // Execução normal
        await service.ExecuteAsync(new RegisterMatchRequest(teamA.Id, teamB.Id, 1, 1));
    }

    /// <summary>
    /// Teste de registro onde time A tem 38 partidas
    /// </summary>
    [Fact]
    public async Task RegisterMatch_TeamA38Matches()
    {
        var teamA = new Team(
            1,
            "Team A",
            38,
            0,
            0,
            10,
            10);

        var teamB = new Team(
            2,
            "Team B",
            10,
            0,
            0,
            10,
            10);

        var service = StartServiceFromList(new List<Team>
        {
            teamA,
            teamB
        });

        await Assert.ThrowsAsync<InvalidMatchException>(() =>
            service.ExecuteAsync(new RegisterMatchRequest(teamA.Id, teamB.Id, 1, 1))
        );
    }

    /// <summary>
    /// Teste de registro onde time B tem 38 partidas
    /// </summary>
    [Fact]
    public async Task RegisterMatch_TeamB38Matches()
    {
        var teamA = new Team(
            1,
            "Team A",
            10,
            0,
            0,
            10,
            10);

        var teamB = new Team(
            2,
            "Team B",
            38,
            0,
            0,
            10,
            10);

        var service = StartServiceFromList(new List<Team>
        {
            teamA,
            teamB
        });

        await Assert.ThrowsAsync<InvalidMatchException>(() =>
            service.ExecuteAsync(new RegisterMatchRequest(teamA.Id, teamB.Id, 1, 1))
        );
    }
}