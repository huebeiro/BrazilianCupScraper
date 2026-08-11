using Huebeiro.BrazilianCup.Domain;
using Huebeiro.BrazilianCup.Domain.Exceptions;

namespace Huebeiro.BrazilianCup.Tests.Domain;

public class MatchTests
{
    /// <summary>
    /// Testando impedimento de uma partida entre o mesmo time
    /// </summary>
    [Fact]
    public void Constructor_SameTeam()
    {
        var team = new Team(
            1,
            "Team A",
            0,
            0,
            0,
            0,
            0);

        Assert.Throws<InvalidMatchException>(() =>
            new Match(
                team,
                team,
                2,
                1));
    }
}

