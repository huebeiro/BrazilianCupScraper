using Huebeiro.BrazilianCup.Domain.Exceptions;

namespace Huebeiro.BrazilianCup.Domain;

public class Match
{
    public Team HomeTeam { get; }
    public Team AwayTeam { get; }
    public short HomeGoals { get; }
    public short AwayGoals { get; }

    public Match(
        Team homeTeam,
        Team awayTeam,
        short homeGoals,
        short awayGoals)
    {
        if (homeTeam.Id == awayTeam.Id)
            throw new InvalidMatchException("A team cannot play against itself.");

        if (homeGoals < 0 || awayGoals < 0)
            throw new InvalidMatchException("Goals cannot be negative.");

        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        HomeGoals = homeGoals;
        AwayGoals = awayGoals;
    }
}