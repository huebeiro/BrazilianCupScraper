using Huebeiro.BrazilianCup.Domain;

namespace Huebeiro.BrazilianCup.Tests.Domain;

public class TeamTests
{
    private static Team CreateEmptyTeam()
    {
        return new Team(
            1,
            "Team A",
            0,
            0,
            0,
            0,
            0);
    }

    /// <summary>
    /// Testando pontuação de vitória
    /// </summary>
    [Fact]
    public void RegisterMatch_Win()
    {
        Team team = CreateEmptyTeam();

        team.RegisterMatch(2, 1);

        Assert.Equal(3, team.Points);
        Assert.Equal(1, team.Wins);
        Assert.Equal(0, team.Draws);
        Assert.Equal(0, team.Losses);
    }

    /// <summary>
    /// Testando pontuação de empate
    /// </summary>
    [Fact]
    public void RegisterMatch_Draw()
    {
        var team = CreateEmptyTeam();

        team.RegisterMatch(1, 1);

        Assert.Equal(1, team.Points);
        Assert.Equal(0, team.Wins);
        Assert.Equal(1, team.Draws);
        Assert.Equal(0, team.Losses);
    }

    /// <summary>
    /// Testando pontuação de derrota
    /// </summary>
    [Fact]
    public void RegisterMatch_Loss()
    {
        var team = CreateEmptyTeam();

        team.RegisterMatch(0, 2);

        Assert.Equal(0, team.Points);
        Assert.Equal(0, team.Wins);
        Assert.Equal(0, team.Draws);
        Assert.Equal(1, team.Losses);
    }

    /// <summary>
    /// Testando atualização das estatísticas
    /// </summary>
    [Fact]
    public void RegisterMatch_StatisticsUpdate()
    {
        var team = new Team(
            1,
            "Team A",
            2,
            1,
            0,
            5,
            2);

        Assert.Equal(3, team.Matches);
        Assert.Equal(2, team.Wins);
        Assert.Equal(1, team.Draws);
        Assert.Equal(0, team.Losses);
        Assert.Equal(5, team.GoalsFor);
        Assert.Equal(2, team.GoalsAgainst);
        Assert.Equal(3, team.GoalDifference);
        Assert.Equal(7, team.Points);

        team.RegisterMatch(3, 1); // Registrando vitória

        Assert.Equal(4, team.Matches);
        Assert.Equal(3, team.Wins);
        Assert.Equal(1, team.Draws);
        Assert.Equal(0, team.Losses);
        Assert.Equal(8, team.GoalsFor);
        Assert.Equal(3, team.GoalsAgainst);
        Assert.Equal(5, team.GoalDifference);
        Assert.Equal(10, team.Points);

        team.RegisterMatch(0, 1); // Registrando derrota

        Assert.Equal(5, team.Matches);
        Assert.Equal(3, team.Wins);
        Assert.Equal(1, team.Draws);
        Assert.Equal(1, team.Losses);
        Assert.Equal(8, team.GoalsFor);
        Assert.Equal(4, team.GoalsAgainst);
        Assert.Equal(4, team.GoalDifference);
        Assert.Equal(10, team.Points);

        team.RegisterMatch(1, 1); // Registrando empate

        Assert.Equal(6, team.Matches);
        Assert.Equal(3, team.Wins);
        Assert.Equal(2, team.Draws);
        Assert.Equal(1, team.Losses);
        Assert.Equal(9, team.GoalsFor);
        Assert.Equal(5, team.GoalsAgainst);
        Assert.Equal(4, team.GoalDifference);
        Assert.Equal(11, team.Points);
    }
}